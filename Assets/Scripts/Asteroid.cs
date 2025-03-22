using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Vida máxima
    [SerializeField] private int maxLife;
    // Vida atual
    private int life;
    // Referência ao Rigidbody2D para movimentação física
    private Rigidbody2D rb;
    // Velocidade de movimento
    [SerializeField] private float movimentSpeed;
    // Ângulo de movimentação (graus)
    [SerializeField] private float movimentAngle;
    // Direção do movimento
    private Vector2 movimentDirection;

    // Propriedade pública para acessar a vida atual do Asteroide
    public int Life
    {
        get { return life; }
        set { life = value; }
    }

    // Propriedade pública para acessar a vida maxima do Asteroide
    public float MaxLife
    {
        get { return maxLife; }
    }

    // Método Start para inicializar o asteroide
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        life = maxLife;

        float radian = movimentAngle * Mathf.Deg2Rad;
        movimentDirection = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    // Atualiza a movimentação do asteroide
    private void Update()
    {
        Moviment();
    }

    // Método para receber dano
    public void TakeDamage(int damage)
    {
        Life -= damage;

        if (life <= 0)
        {
            life = 0;
            Destroy(gameObject);
        }
    }

    // Define o movimento do asteroide
    private void Moviment()
    {
        rb.velocity = movimentDirection * movimentSpeed;
    }

    // Detecta colisões com tiros do jogador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Shot")) return;

        Shot shot = collision.gameObject.GetComponent<Shot>();
        if (shot == null || !shot.IsShotPlayer) return;

        TakeDamage(shot.Damage);
        collision.gameObject.SetActive(false);
    }
}
