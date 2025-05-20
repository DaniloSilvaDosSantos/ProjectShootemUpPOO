using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class MPDA : MonoBehaviour
{
    public static MPDA Instance { get; private set; }

    private bool historyInitialized = false;

    // Variavel para armazenar o local do arquivo csv, onde vai ter os registros do desenpenho do jogador
    private string filePath;

    // Pesos das métricas ajustáveis, a soma dos pesos não devem dar um valor direfente de 1
    [SerializeField] private float damageWeight = 0.6f;
    [SerializeField] private float resultWeight = 0.4f;

    // Coeficiente de suavização, quanto maior mais o modelo reage rapidamente
    [SerializeField] private float smoothingFactor = 0.3f;

    // Histórico dos perfis anteriores para suavizar o valor final
    private List<float> profileHistory = new List<float>();

    // Valor maximo de perfils que seram armazenados no historico
    [SerializeField] private int maxProfileHistoryCount = 10;

    // Valor maximo de dano que é possivel ser sofrido
    [SerializeField] private float maxDamageTaken = 4f;

    private void Awake()
    {
        //Padrão Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantém o MPDA entre cenas
        }
        else
        {
            Destroy(gameObject); // Destroy caso já exista um
        }
    }

    void Start()
    {
        // Caminho do arquivo CSV onde os dados de desempenho estão armazenados
        filePath = Application.persistentDataPath + "/dados_jogador.csv";
    }

    // Função principal que lê os dados, gera métricas e calcula o perfil de dificuldade
    public void CalculateProfile()
    {
        // Lê todas as linhas do arquivo, ignorando o cabeçalho
        var lines = File.ReadAllLines(filePath).Skip(1).ToList();
        if (lines.Count == 0)
        {
            GameController.Instance.DifficultLevel = 5f; // valor neutro se não houver dados
            return;
        } 

        List<MatchData> matches = new List<MatchData>();

        foreach (var line in lines)
        {
            var parts = line.Split(',');
            float result = parts[2] == "Victory" ? 1f : 0f;
            int damage = int.Parse(parts[3]);

            matches.Add(new MatchData { Result = result, Damage = damage });
        }

        // Inicializa histórico com base nos dados anteriores, somente na primeira vez
        if (!historyInitialized)
        {
            InitializeHistory(matches);
            historyInitialized = true;
        }

        string debugHistory = "HistoricoPerfils: ";
        foreach (var item in profileHistory)
        {
            debugHistory += item.ToString()+", ";
        }
        Debug.Log(debugHistory);

        // Pega apenas a última partida para cálculo
        var lastMatch = matches.Last();

        // Calculando as metricas
        float lastMatchResult = lastMatch.Result;
        float lastMatchDamage = 1f - Mathf.Clamp01((float)lastMatch.Damage / maxDamageTaken);

        // Calcula o perfil bruto com base nos pesos das métricas
        float rawProfile = (lastMatchResult * resultWeight + lastMatchDamage * damageWeight) * 10f;

        // Suavização com base no histórico (evita oscilações abruptas)
        float historicalAverage = profileHistory.Count > 0 ? profileHistory.Average() : rawProfile;
        float finalProfile = smoothingFactor * rawProfile + (1 - smoothingFactor) * historicalAverage;

        // Adiciona o perfil final ao histórico
        profileHistory.Add(finalProfile);
        if (profileHistory.Count > maxProfileHistoryCount) profileHistory.RemoveAt(0);

        Debug.Log("Media historica dos perfils: " + historicalAverage);
        Debug.Log("Perfil bruto: " + rawProfile);
        Debug.Log("Perfil Final: " + finalProfile);

        GameController.Instance.DifficultLevel = finalProfile;
    }

    // Classe auxiliar para representar os dados de uma partida
    private class MatchData
    {
        public float Result; // 1 se vitória, 0 se derrota
        public int Damage;
    }

    // Função para reconstruir o histórico com base nos dados do registro
    private void InitializeHistory(List<MatchData> allMatches)
    {
        Debug.Log("Inicializando histórico de perfil com dados anteriores do CSV!");

        var pastChunks = allMatches.TakeLast(maxProfileHistoryCount);
        foreach (var m in pastChunks)
        {
            float result = m.Result;
            float damage = 1f - Mathf.Clamp01((float)m.Damage / maxDamageTaken);
            float profile = (result * resultWeight + damage * damageWeight) * 10f;

            Debug.Log($"Adicionando ao histórico: result={result}, damage={damage}, profile={profile}");

            profileHistory.Add(profile);
        }
    }
}

