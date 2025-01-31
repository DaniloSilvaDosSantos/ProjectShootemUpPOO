using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe responsável por controlar o movimento do jogador
public class PlayerMoviment : MonoBehaviour
{
    // Velocidade máxima do jogador, configurável pelo Inspector
    [SerializeField] private float maxSpeed = 5f;

    // Referência ao script do jogador (Player)
    private Player player;

    // Referência ao script responsável pelos inputs do jogador (PlayerInputs)
    private PlayerInputs playerInputs;

    // Método chamado quando o objeto é inicializado
    private void Start()
    {
        // Obtém a referência ao componente Player no mesmo GameObject
        player = GetComponent<Player>();

        // Obtém a referência ao componente PlayerInputs no mesmo GameObject
        playerInputs = GetComponent<PlayerInputs>();
    }

    // Método chamado a cada frame para atualizar o movimento do jogador
    private void Update()
    {
        Move();
    }

    // Método responsável por calcular e aplicar a velocidade do jogador
    private void Move()
    {
        // Obtém a direção do input do jogador
        Vector2 direction = playerInputs.GetInputDirection();

        // Calcula um multiplicador de velocidade com base na intensidade do input
        float speedMultiplier = direction.magnitude;

        // Normaliza a direção para manter a velocidade consistente e aplica o multiplicador
        Vector2 velocity = direction.normalized * maxSpeed * speedMultiplier;

        // Atualiza a velocidade do jogador
        player.UpdateVelocity(velocity);
    }
}
