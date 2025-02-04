using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemie : MonoBehaviour
{
    [SerializeField] private int maxLife;
    private int life;
    private Rigidbody2D rb;
    [SerializeField] private float movimentSpeed;
    [SerializeField] float movimentAngle;
    private Vector2 movimentDirection;
    [SerializeField] GameObject shotPrefab;
    [SerializeField] private float shotCouldown;
    [SerializeField] private float shotVelocity;
    [SerializeField] private int shotDamage;
    [SerializeField] private float shotAngle;
    [SerializeField] private float shotLife;

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

        InvokeRepeating("Shot", shotCouldown, shotCouldown);
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

    protected virtual void Moviment()
    {
        rb.velocity = movimentDirection * movimentSpeed;
    }

    private void Shot()
    {
        GameObject shot = Instantiate(shotPrefab, transform.position, Quaternion.identity);

        shot.GetComponent<Shot>().Initialize(shotVelocity, shotDamage, false, shotAngle, shotLife);
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
