using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoamingShot : Shot
{
    // Duração do efeito de perseguição
    private float homingDuration; 
    // Posição do alvo
    private Vector2 targetLocation; 
    // Referência ao transform do alvo
    private Transform targetTransform; 

     // Inicializa os parâmetros do projétil teleguiado
    public void Initialize(float velocity, int damage, bool isShotPlayer, float angle, float shotLife, float homingDuration, Vector2 targetLocation)
    {
        // Herda do Initialize original da classe Shot
        base.Initialize(velocity, damage, isShotPlayer, angle, shotLife);
        // Define os atributos do tiro com os valores passados
        this.homingDuration = homingDuration;
        this.targetLocation = targetLocation;
    }

    private void Start()
    {
        // Encontra o alvo ao iniciar
        FindTarget();
    }

    protected override void Update()
    {
        base.Update();

        if (homingDuration > 0)
        {
            AdjustDirection(); // Ajusta a direção do projétil se o tempo de perseguição não acabou
        }

        ReduceHomingDuration(); // Reduz a duração do efeito de perseguição

        Moviment(); // Movimenta o projétil
    }

    // Metodo para encontrar o jogador como alvo do projétil
    private void FindTarget()
    {
        // Pega a referencia do objeto do jogador
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Coloca a possição do jogador como alvo do tiro
            targetTransform = player.transform;
        }
    }

    // Ajusta a direção do projétil para seguir o alvo
    private void AdjustDirection()
    {
        if (targetTransform != null)
        {
            // Atualiza a posição
            targetLocation = targetTransform.position;
        }

        // Calcula a nova direção normalizada
        Vector2 newDirection = (targetLocation - (Vector2)transform.position).normalized;
        direction = newDirection;
        // Calcula o ângulo da rotação
        angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
        // Aplica a rotação ao projétil
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    // Metodo que reduz o tempo de perseguição ao longo do tempo
    private void ReduceHomingDuration()
    {
        if (homingDuration > 0)
        {
            homingDuration -= Time.deltaTime;
        }
    }
}
