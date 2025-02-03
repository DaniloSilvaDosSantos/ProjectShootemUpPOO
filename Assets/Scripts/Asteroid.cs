using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private int maxLife;
    private int life;
    private Rigidbody2D rb;
    [SerializeField] private float movimentSpeed;
    [SerializeField] float movimentAngle;
    private Vector2 movimentDirection;

    public int Life
    {
        get { return life; }
        set { life = value; }
    }

    public float MaxLife
    {
        get { return maxLife; }
    }

    private void Start()
    {
        rb =  GetComponent<Rigidbody2D>();

        life = maxLife;

        float radian = movimentAngle * Mathf.Deg2Rad;
        movimentDirection = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    private void Update()
    {
        Moviment();
    }

    public void TakeDamage(int damage)
    {
        Life = life - damage;

        if (life <= 0)
        {
            life = 0;
            Destroy(gameObject);
        }
    }

    private void Moviment()
    {
        rb.velocity = movimentDirection * movimentSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Shot")) return;

        Shot shot = collision.gameObject.GetComponent<Shot>();
        if (shot == null || !shot.IsShotPlayer) return;

        TakeDamage(shot.Damage);
        Destroy(collision.gameObject);
    }
}
