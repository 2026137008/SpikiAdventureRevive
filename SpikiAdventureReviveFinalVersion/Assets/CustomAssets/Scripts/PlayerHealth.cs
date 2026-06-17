using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hitSound;
    private SpriteRenderer spriteRenderer;
    [Header("체력")]
    public int maxHP = 10;
    public int currentHP;

    [Header("무적 시간")]
    public float invincibleTime = 0.5f;

    private bool isInvincible = false;

    void Start()
    {
        currentHP = maxHP;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    IEnumerator DamageFlash()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.15f);

        spriteRenderer.color = Color.white;
    }

    IEnumerator Invincible()
    {
        isInvincible = true;

        for(int i = 0; i < 5; i++)
        {
            spriteRenderer.color = new Color(1,1,1,0.3f);

            yield return new WaitForSeconds(0.1f);

            spriteRenderer.color = Color.white;

            yield return new WaitForSeconds(0.1f);
        }

        isInvincible = false;
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)
            return;

        currentHP -= damage;

        audioSource.PlayOneShot(hitSound);

        StartCoroutine(DamageFlash());
        StartCoroutine(Invincible());

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHP += amount;

        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    void Die()
    {
        Debug.Log("플레이어 사망");

        gameObject.SetActive(false);

        SceneManager.LoadScene("GameOver");
    }
}