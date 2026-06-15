using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;
    public int damage = 1;

    private Vector2 moveDirection;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    void Update()
    {
        transform.position +=
            (Vector3)(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyAI enemy = other.GetComponent<EnemyAI>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}