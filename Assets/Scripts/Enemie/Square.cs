using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Square : Enemie
{
    [SerializeField] private bool moveRight; // Define a direção do movimento e do tiro

    // Método Start sobrescrito para definir a direção do movimento e do tiro
    protected override void Start()
    {
        base.Start();

        if (moveRight)
        {
            movimentAngle = 315f; // Move para a direita
            shotAngle = 225f; // Dispara na diagonal esquerda
        }
        else
        {
            movimentAngle = 225f; // Move para a esquerda
            shotAngle = 315f; // Dispara na diagonal direita
        }

        // Converte o ângulo de movimentação em direção
        float radian = movimentAngle * Mathf.Deg2Rad;
        movimentDirection = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

        // Inicia os disparos automáticos
        InvokeRepeating("Shot", shotCouldown, shotCouldown);
    }
}
