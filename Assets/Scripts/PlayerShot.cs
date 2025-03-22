using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

// Classe responsável pelos tiros e bombas do jogador
public class PlayerShot : MonoBehaviour
{
    // Nome do tiro do jogador(vai ser usado na hora de chamar o metodo do ShotObjectPooler que vai ativar os tiros)
    [SerializeField] private string shotType;
    //Referencia para o ShotObjectPooler
    private ShotObjectPooler shotPooler;

    // Prefab da bomba do jogador
    [SerializeField] private GameObject bombPrefab;

    // Quantidade atual de bombas disponíveis
    private int bombs;

    // Quantidade máxima de bombas que o jogador pode ter
    [SerializeField] private int maxBombs;

    // Tempo de espera entre cada disparo
    [SerializeField] private float shotCouldown;

    // Velocidade do tiro
    [SerializeField] private float shotVelocity;

    // Dano do tiro
    [SerializeField] private int shotDamage;

    // Ângulo inicial do tiro
    [SerializeField] private float shotAngle;

    // Tempo de vida do tiro antes de desaparecer
    [SerializeField] private float shotLife;

    // Referência ao script que gerencia os inputs do jogador
    private PlayerInputs playerInputs;

    // Propriedade para acessar o número máximo de bombas
    public int MaxBombs
    {
        get { return maxBombs; }
    }

    // Propriedade para acessar e modificar a quantidade de bombas do jogador
    public int Bombs
    {
        get { return bombs; }
        set
        {
            // Garante que o jogador não tenha mais bombas do que o máximo permitido
            if (value > MaxBombs)
            {
                bombs = MaxBombs;
            }
            else
            {
                bombs = value;
            }
        }
    }

    // Método chamado na inicialização do objeto
    private void Start()
    {
        // Obtém a referência ao script de inputs do jogador
        playerInputs = GetComponent<PlayerInputs>();

        // Obtem a referência ao script do ShotObjectPooler
        shotPooler = GameObject.Find("ShotObjectPooler").GetComponent<ShotObjectPooler>();

        // Inicializa a quantidade de bombas com o valor máximo
        bombs = maxBombs;
        
        // Dispara repetidamente a função "Shot" com intervalo definido pelo cooldown
        InvokeRepeating("Shot", shotCouldown, shotCouldown);
    }

    // Método chamado a cada frame
    private void Update()
    {
        // Se o botão da bomba foi pressionado, joga uma bomba
        if (playerInputs.BombPressed == true)
        {
            ThrowBomb();
        }
    }

    // Método responsável por instanciar e configurar um tiro
    private void Shot()
    {
        // chama um tiro do object pool na posição e rotação do jogador
        GameObject shot = shotPooler.SpawnFromPool(shotType);
        shot.transform.position = transform.position;
        shot.transform.rotation = Quaternion.identity;

        // Configura os parâmetros do tiro (velocidade, dano, tiro do jogador, angulo, tempo de vida)
        shot.GetComponent<Shot>().Initialize(shotVelocity, shotDamage, true, shotAngle, shotLife);
    }

    // Método responsável por lançar uma bomba
    private void ThrowBomb()
    {
        Debug.Log("Bomba");

        // Reseta a variável de input para evitar múltiplas ativações seguidas
        playerInputs.BombPressed = false;
    }
}
