using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1.5f;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Collider2D[] enemies =
                Physics2D.OverlapCircleAll(
                    transform.position,
                    attackRange
                );

            foreach(Collider2D enemy in enemies)
            {
                EnemyAI ai =
                    enemy.GetComponent<EnemyAI>();

                if(ai != null)
                {
                    ai.TakeDamage(1);
                }
            }
        }
    }
}