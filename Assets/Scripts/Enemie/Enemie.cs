using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemie : MonoBehaviour
{
    // Vida máxima do inimigo
    [SerializeField] private int maxLife;
    // Vida atual do inimigo
    private int life; 
    // Referência ao Rigidbody2D para movimentação física
    private Rigidbody2D rb;
    // Velocidade de movimento do inimigo
    [SerializeField] protected float movimentSpeed;
    [SerializeField] protected float[] movimentSpeeds; 
    // Ângulo de movimentação (graus)
    [SerializeField] protected float movimentAngle; 
    // Direção do movimento
    protected Vector2 movimentDirection; 

    // Nome do tiro do inimigo(vai ser usado na hora de chamar o metodo do ShotObjectPooler que vai ativar os tiros)
    [SerializeField] protected string shotType;
    //Referencia para o ShotObjectPooler
    protected ShotObjectPooler shotPooler; 
    // Tempo entre disparos
    [SerializeField] protected float shotCouldown;
    [SerializeField] protected float[] shotCouldowns; 
    // Velocidade do tiro
    [SerializeField] protected float shotVelocity;
    [SerializeField] protected float[] shotVelocitys; 
    // Dano causado pelo tiro
    [SerializeField] protected int shotDamage; 
    // Ângulo de disparo do tiro
    [SerializeField] protected float shotAngle; 
    // Tempo de vida do tiro
    [SerializeField] protected float shotLife;
    
    [SerializeField] protected int[] difficultLevels;

    // Propriedade pública para acessar a vida atual do inimigo
    public int Life
    {
        get { return life; }
        set { life = value; }
    }

    // Propriedade pública para acessar a vida máxima
    public float MaxLife
    {
        get { return maxLife; }
    }

    // Método chamado quando o inimigo é criado
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtém o Rigidbody2D do inimigo

        AdaptingParamters();

        shotPooler = GameObject.Find("ShotObjectPooler").GetComponent<ShotObjectPooler>();

        life = maxLife; // Define a vida inicial

        // Converte o ângulo de movimentação de graus para radianos e define a direção do movimento
        float radian = movimentAngle * Mathf.Deg2Rad;
        movimentDirection = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

        // Inicia os disparos automaticamente com um intervalo de 'shotCouldown'
        InvokeRepeating("Shot", shotCouldown, shotCouldown);
    }

    // Método chamado a cada frame
    private void Update()
    {
        Moviment(); // Atualiza a movimentação do inimigo
    }

    protected virtual void AdaptingParamters()
    {
        GameController gameController = Object.FindFirstObjectByType<GameController>();

        for(int i=0; i < difficultLevels.Length; i++)
        {
            if(difficultLevels[i] <= gameController.DifficultLevel)
            {
                movimentSpeed = movimentSpeeds[i];
                shotCouldown = shotCouldowns[i];
                shotVelocity = shotVelocitys[i];

                i = difficultLevels.Length;
            }
        }
    }

    // Método para receber dano
    public void TakeDamage(int damage)
    {
        Life -= damage; // Reduz a vida

        if (life <= 0) // Se a vida chegar a zero, o inimigo é destruído
        {
            life = 0;
            Destroy(gameObject);
        }
    }

    // Método para movimentação do inimigo
    protected virtual void Moviment()
    {
        rb.linearVelocity = movimentDirection * movimentSpeed; // Move o inimigo na direção e velocidade definidas
    }

    //Metodo para disparar tiros
    protected virtual void Shot()
    {
        // chama um tiro do object pool na posição e rotação do inimigo
        GameObject shot = shotPooler.SpawnFromPool(shotType);
        shot.transform.position = transform.position;
        shot.transform.rotation = Quaternion.identity;

        // Configura os parâmetros do tiro (velocidade, dano, tiro do inimigo, angulo, tempo de vida)
        shot.GetComponent<Shot>().Initialize(shotVelocity, shotDamage, false, shotAngle, shotLife);
    }

    // Detecta colisões com outros objetos
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se colidiu com um tiro
        if (!collision.gameObject.CompareTag("Shot")) return;

        // Obtém o script do tiro e verifica se foi disparado pelo jogador
        Shot shot = collision.gameObject.GetComponent<Shot>();
        if (shot == null || !shot.IsShotPlayer) return;

        TakeDamage(shot.Damage); // Aplica dano ao inimigo
        collision.gameObject.SetActive(false); // Desabilita o tiro
    }
}
