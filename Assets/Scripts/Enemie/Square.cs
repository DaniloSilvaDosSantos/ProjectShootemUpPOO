using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Square : Enemie
{
    [SerializeField] private bool moveRight; // Define a direção do movimento e do tiro

    protected override void Start()
    {
        base.Start();

        if (moveRight)
        {
            movimentAngle = 315f;
            shotAngle = 225f;
        }
        else
        {
            movimentAngle = 225f;
            shotAngle = 315f;
        }

        float radian = movimentAngle * Mathf.Deg2Rad;
        movimentDirection = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

        InvokeRepeating("Shot", shotCouldown, shotCouldown);
    }
}
