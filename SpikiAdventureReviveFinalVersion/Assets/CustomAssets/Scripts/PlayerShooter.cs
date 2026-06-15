using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("총알")]
    public GameObject bulletPrefab;

    [Header("발사 위치")]
    public Transform firePoint;

    [Header("연사 속도")]
    public float shootCooldown = 0.3f;

    private float cooldownTimer;

    // 플레이어가 바라보는 방향
    private Vector2 lookDirection = Vector2.down;

    void Start()
    {
        Debug.Log("PlayerShooter 시작");
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        UpdateLookDirection();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("발사!");
            Shoot();
        }
    }

    void UpdateLookDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 이동 중일 때만 방향 갱신
        if (horizontal != 0 || vertical != 0)
        {
            lookDirection = new Vector2(horizontal, vertical).normalized;
        }
    }

    void Shoot()
    {
        if (cooldownTimer < shootCooldown)
            return;

        cooldownTimer = 0f;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.identity);

        PlayerBullet bulletScript =
            bullet.GetComponent<PlayerBullet>();

        bulletScript.SetDirection(lookDirection);
    }
}