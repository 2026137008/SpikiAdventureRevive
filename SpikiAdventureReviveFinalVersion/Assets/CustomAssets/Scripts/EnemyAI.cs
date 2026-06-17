using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("다음 씬")]
    public string nextSceneName;

    [Header("보스 여부")]
    public bool isBoss = false;

    private DoorPortal doorPortal;
    private Collider2D enemyCollider;
    private SpriteRenderer spriteRenderer;
    private bool isDead = false;
    public GameObject hpBarCanvas;

    [Header("이동")]
    public float moveSpeed = 2f;

    [Header("감지 범위")]
    public float detectRange = 5f;

    [Header("체력")]
    public int maxHP = 3;

    [Header("공격")]
    public int contactDamage = 1;

    public float damageInterval = 1f;

    public Slider hpBar;

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
        doorPortal = FindObjectOfType<DoorPortal>();
        enemyCollider = GetComponent<Collider2D>();
        currentHP = maxHP;

        if (hpBarCanvas != null)
        {
            hpBarCanvas.SetActive(false);
        }

        spriteRenderer = GetComponent<SpriteRenderer>();

        hpBar.maxValue = maxHP;
        hpBar.value = currentHP;

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
        if (isDead)
            return;

        hpBarCanvas.SetActive(true);

        currentHP -= damage;

        hpBar.value = currentHP;

        StartCoroutine(DamageFlash());

        if(currentHP <= 0)
        {
            StartCoroutine(Die());
        }
    }
    IEnumerator Die()
    {
        isDead = true;

        moveSpeed = 0f;

        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }

        if (hpBarCanvas != null)
        {
            hpBarCanvas.SetActive(false);
        }

        Color color = spriteRenderer.color;

        float fadeTime = 1f;

        float timer = 0f;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(
                1f,
                0f,
                timer / fadeTime);

            spriteRenderer.color = color;

            yield return null;
        }

        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.AddProgress();
        }

        if (isBoss)
        {
            yield return new WaitForSeconds(0.7f);

            FadeManager.Instance.LoadScene(nextSceneName);
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
    IEnumerator DamageFlash()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.08f);

        spriteRenderer.color = Color.white;
    }
}