using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : Enemie
{
    private Vector2 targetPosition; // Posição do alvo (jogador)
    [SerializeField] private float homingDuration; // Tempo que o tiro segue o jogador
    private Transform player; // Referência ao jogador

    // Método Start sobrescrito para encontrar o jogador
    protected override void Start()
    {
        base.Start(); // Chama o Start da classe base (Enemie)
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Encontra o jogador pela tag
    }

    // Obtém a posição atual do jogador
    private void GetPlayerPosition()
    {
        if (player != null)
        {
            targetPosition = player.position;
        }
    }

    // Método de tiro sobrescrito para seguir o jogador
    protected override void Shot()
    {
        GetPlayerPosition(); // Atualiza a posição do jogador

        // Cria o tiro e define que ele segue o jogador
        GameObject shot = Instantiate(shotPrefab, transform.position, Quaternion.identity);
        shot.GetComponent<HoamingShot>().Initialize(shotVelocity, shotDamage, false, shotAngle, shotLife, homingDuration, targetPosition);
    }
}
