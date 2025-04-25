using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : Enemie
{
    private Vector2 targetPosition; // Posição do alvo (jogador)
    [SerializeField] private float homingDuration; // Tempo que o tiro segue o jogador
    [SerializeField] private float[] homingDurations;
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

        // chama um tiro do object pool na posição e rotação do inimigo
        GameObject shot = shotPooler.SpawnFromPool(shotType);
        shot.transform.position = transform.position;
        shot.transform.rotation = Quaternion.identity;

        // Configura os parâmetros do tiro (velocidade, dano, tiro do inimigo, angulo, tempo de vida)
        shot.GetComponent<HoamingShot>().Initialize(shotVelocity, shotDamage, false, shotAngle, shotLife, homingDuration, player.position);
    }

    protected override void AdaptingParamters()
    {
        GameController gameController = Object.FindFirstObjectByType<GameController>();

        for(int i=0; i < difficultLevels.Length; i++)
        {
            if(difficultLevels[i] <= gameController.DifficultLevel)
            {
                movimentSpeed = movimentSpeeds[i];
                shotCouldown = shotCouldowns[i];
                shotVelocity = shotVelocitys[i];
                homingDuration = homingDurations[i];
            }
        }
    }
}
