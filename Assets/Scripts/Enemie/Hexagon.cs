using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : Enemie
{
    private Vector2 targetPosition;
    [SerializeField] private float homingDuration;
    private Transform player;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void GetPlayerPosition()
    {
        if (player != null)
        {
            targetPosition = player.position;
        }
    }

    protected override void Shot()
    {
        GetPlayerPosition();
        GameObject shot = Instantiate(shotPrefab, transform.position, Quaternion.identity);
        shot.GetComponent<HoamingShot>().Initialize(shotVelocity, shotDamage, false, shotAngle, shotLife, homingDuration, targetPosition);
    }
}
