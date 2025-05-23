using UnityEngine;

public class PlayerAIController : MonoBehaviour
{
    private Player player;
    private PlayerMoviment playerMoviment;
    [SerializeField] private float dangerRangeY = 3f;
    [SerializeField] private float dangerRangeX = 1f;

    void Start()
    {
        player = GetComponent<Player>();
        playerMoviment = GetComponent<PlayerMoviment>();
    }

    
    public void AIBrain()
    {
        Vector2 aiDirection = Vector2.zero;

        if (IsDangerAbove(out float dodgeDir))
        {
            aiDirection.x = dodgeDir;
        }
        else
        {
            GameObject target = FindClosestEnemy();
            if (target != null)
            {
                float xDiff = target.transform.position.x - transform.position.x;
                if (Mathf.Abs(xDiff) > 0.1f)
                    aiDirection.x = Mathf.Sign(xDiff);
            }
        }

        Vector2 velocity = new Vector2(aiDirection.x * playerMoviment.MaxSpeed, 0f);
        player.UpdateVelocity(velocity);
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(currentPosition, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }

    private bool IsDangerAbove(out float dangerDirection)
    {
        Vector3 pos = transform.position;
        dangerDirection = 0f;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos + Vector3.up * dangerRangeY / 2, new Vector2(dangerRangeX, dangerRangeY), 0f);
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Enemy") || col.CompareTag("Shot"))
            {
                Shot shot = col.GetComponent<Shot>();
                if (col.CompareTag("Enemy") || (shot != null && !shot.IsShotPlayer))
                {
                    dangerDirection = (col.transform.position.x > pos.x) ? -1f : 1f;
                    return true;
                }
            }
        }

        return false;
    }
}
