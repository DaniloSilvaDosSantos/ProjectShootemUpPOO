using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

// Classe responsável pelo comportamento do tiro no jogo
public class Shot : MonoBehaviour
{
    // Componente de física do tiro
    private Rigidbody2D rb;

    // Velocidade do tiro
    private float velocity;

    // Dano causado pelo tiro
    private int damage;

    // Indica se o tiro foi disparado pelo jogador (true) ou por um inimigo (false)
    private bool isShotPlayer;

    // Ângulo de disparo do tiro
    protected float angle;

    // Direção do movimento do tiro
    protected Vector2 direction;

    // Tempo de vida do tiro antes de ser destruído
    private float shotLife;

    // Propriedade para acessar e modificar a velocidade do tiro
    public float Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    // Propriedade para acessar e modificar o dano do tiro
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    // Propriedade para acessar o valor que representa se o tiro foi disparado pelo jogador
    public bool IsShotPlayer
    {
        get { return isShotPlayer; }
    }

    // Propriedade para acessar e modificar o ângulo do tiro
    public float Angle
    {
        get { return angle; }
        set { angle = value; }
    }

    // Propriedade para acessar e modificar o tempo de vida do tiro
    public float ShotLife
    {
        get { return shotLife; }
        set { shotLife = value; }
    }

    // Método de inicialização do tiro com os parâmetros necessários
    public virtual void Initialize(float velocity, int damage, bool isShotPlayer, float angle, float shotLife)
    {
        // Obtém a referência ao Rigidbody2D do tiro
        rb = GetComponent<Rigidbody2D>();

        // Define os atributos do tiro com os valores passados
        this.velocity = velocity;
        this.damage = damage;
        this.isShotPlayer = isShotPlayer;
        this.angle = angle;
        this.shotLife = shotLife;

        // Rotaciona o tiro para apontar na direção correta
        transform.rotation = Quaternion.Euler(0, 0, this.angle);

        // Converte o ângulo de graus para radianos e calcula a direção do tiro
        float radian = this.angle * Mathf.Deg2Rad;
        direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

        // Agenda a destruição do tiro após um determinado tempo de vida
        Invoke("DestroyShot", shotLife);
    }

    // Método chamado a cada frame
    protected virtual void Update()
    {
        // Aplica o movimento ao tiro
        Moviment();
    }

    // Método responsável por movimentar o tiro na direção definida
    protected void Moviment()
    {
        rb.velocity = direction * velocity;
    }

    // Método que destrói o tiro após o tempo de vida expirar
    private void DestroyShot()
    {
        Destroy(gameObject);
    }
}

