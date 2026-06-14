using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("이동")]
    public float moveSpeed = 2f;

    [Header("감지 범위")]
    public float detectRange = 5f;

    [Header("체력")]
    public int maxHP = 3;

    [Header("공격")]
    public int contactDamage = 1;

    public float damageInterval = 1f;

    private int currentHP;

    private Transform player;

    private float damageTimer = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health =
                other.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.TakeDamage(contactDamage);
            }

            damageTimer = 0f;
        }
    }

    void Start()
    {
        currentHP = maxHP;

        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player == null)
            return;

        float distance =
            Vector2.Distance(
                transform.position,
                player.position);

        if (distance <= detectRange)
        {
            transform.position =
                Vector2.MoveTowards(
                    transform.position,
                    player.position,
                    moveSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.AddProgress();
        }

        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= damageInterval)
            {
                PlayerHealth health =
                    other.GetComponent<PlayerHealth>();

                if (health != null)
                {
                    health.TakeDamage(contactDamage);
                }

                damageTimer = 0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            damageTimer = 0f;
        }
    }
}