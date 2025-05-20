using System.IO;
using UnityEngine;
using System;

public class GameSessionLogger : MonoBehaviour
{
    // Variavel para armazenar o local do arquivo csv, onde vai ser registrado o desenpenho do jogador
    private string filePath;

    void Start()
    {
        // Definindo qual é o caminho do arquivo
        filePath = Application.persistentDataPath + "/dados_jogador.csv";

        // Caso o arquivo não existe vai ser criado um cabeçalho
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "DataHora,Nivel,Resultado,DanoSofrido\n");
        }
    }

    // Metodo para registrar o desenpenho do jogador no ultimo level no arquivo csv
    public void RegisterResult(int level, bool playerWon, int damageTaken)
    {
        string result = playerWon ? "Victory" : "Defeat";
        string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string line = $"{date},{level},{result},{damageTaken}";

        File.AppendAllText(filePath, line + "\n");

        // Enviar os dados para o MPDA
        GameObject.FindAnyObjectByType<MPDA>().CalculateProfile();

    }
}

