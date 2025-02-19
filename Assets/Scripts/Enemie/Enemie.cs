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
    [SerializeField] private float movimentSpeed; 
    // Ângulo de movimentação (graus)
    [SerializeField] protected float movimentAngle; 
    // Direção do movimento
    protected Vector2 movimentDirection; 

    // Prefab do tiro que o inimigo dispara
    [SerializeField] protected GameObject shotPrefab; 
    // Tempo entre disparos
    [SerializeField] protected float shotCouldown; 
    // Velocidade do tiro
    [SerializeField] protected float shotVelocity; 
    // Dano causado pelo tiro
    [SerializeField] protected int shotDamage; 
    // Ângulo de disparo do tiro
    [SerializeField] protected float shotAngle; 
    // Tempo de vida do tiro
    [SerializeField] protected float shotLife; 

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
        rb.velocity = movimentDirection * movimentSpeed; // Move o inimigo na direção e velocidade definidas
    }

    // Método para disparar tiros
    protected virtual void Shot()
    {
        // Instancia um tiro na posição do inimigo
        GameObject shot = Instantiate(shotPrefab, transform.position, Quaternion.identity);
        
        // Inicializa o tiro com seus atributos
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
        Destroy(collision.gameObject); // Destroi o tiro após atingir o inimigo
    }
}
