using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe que representa o jogador no jogo
public class Player : MonoBehaviour
{
    // Quantidade máxima de vida do jogador, configurável pelo Inspector
    [SerializeField] private int maxLife = 4;

    // Vida atual do jogador
    private int life;

    // Tempo de invulnerabilidade após sofrer dano, configurável pelo Inspector
    [SerializeField] private float invulnerability = 2f;

    // Tempo restante de invulnerabilidade
    private float currentInvulnerability = 0f;

    // Referência ao Rigidbody2D do jogador, usada para manipular a física
    private Rigidbody2D rb;

    // Propriedade para acessar e modificar a vida do jogador
    public int Life
    {
        get { return life; }
        set { life = value; }
    }

    // Propriedade somente leitura que retorna a vida máxima
    public float MaxLife
    {
        get { return maxLife; }
    }

    // Propriedade para acessar e modificar o tempo atual de invulnerabilidade
    public float CurrentInvulnerability
    {
        get { return currentInvulnerability; }
        set { currentInvulnerability = value; }
    }

    // Propriedade para acessar e modificar o tempo total de invulnerabilidade
    public float Invulnerability
    {
        get { return invulnerability; }
        set { invulnerability = value; }
    }

    // Método chamado quando o objeto é inicializado
    private void Start()
    {
        // Define a vida inicial do jogador como a vida máxima
        life = maxLife;

        // Obtém a referência ao Rigidbody2D do jogador
        rb = GetComponent<Rigidbody2D>();
    }

    // Método chamado a cada frame
    private void Update()
    {
        // Atualiza o tempo de invulnerabilidade
        HandleInvulnerability();
    }

    // Método para reduzir a vida do jogador ao sofrer dano
    public void TakeDamage(int damage)
    {
        // Reduz a vida do jogador pelo valor do dano recebido
        Life = life - damage;

        // Reinicia o tempo de invulnerabilidade
        currentInvulnerability = invulnerability;

        // Se a vida do jogador chegar a 0 ou menos, ele é destruído
        if (life <= 0)
        {
            life = 0;
            Destroy(gameObject);
        }
    }

    // Método responsável por diminuir o tempo de invulnerabilidade ao longo do tempo
    private void HandleInvulnerability()
    {
        if (currentInvulnerability > 0)
        {
            currentInvulnerability -= Time.deltaTime;
        }
    }

    // Método para atualizar a velocidade do jogador
    public void UpdateVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    // Método chamado ao colidir com outro objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Parar o codigo de colisão se o jogador ainda está invulneravel
        if (currentInvulnerability <= 0) return;

        // Se colidir com um inimigo, recebe dano
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("Shot")) // Caso colidir com um tiro do inimigo, recebe dano
        {
            Shot shot = collision.gameObject.GetComponent<Shot>();
            if(!shot.IsShotPlayer) TakeDamage(shot.Damage);
        }
    }
}