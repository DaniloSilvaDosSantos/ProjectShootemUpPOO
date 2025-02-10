using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoamingShot : Shot
{
    private float homingDuration;
    private Vector2 targetLocation;
    private Transform targetTransform;

    public void Initialize(float velocity, int damage, bool isShotPlayer, float angle, float shotLife, float homingDuration, Vector2 targetLocation)
    {
        base.Initialize(velocity, damage, isShotPlayer, angle, shotLife);
        this.homingDuration = homingDuration;
        this.targetLocation = targetLocation;
    }

    private void Start()
    {
        FindTarget();
    }

    protected override void Update()
    {
        base.Update();

        if (homingDuration > 0)
        {
            AdjustDirection();
        }

        ReduceHomingDuration();

        Moviment();
    }

    private void FindTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            targetTransform = player.transform;
        }
    }

    private void AdjustDirection()
    {
        if (targetTransform != null)
        {
            targetLocation = targetTransform.position;
        }

        Vector2 newDirection = (targetLocation - (Vector2)transform.position).normalized;
        direction = newDirection;
        angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void ReduceHomingDuration()
    {
        if (homingDuration > 0)
        {
            homingDuration -= Time.deltaTime;
        }
    }
}
