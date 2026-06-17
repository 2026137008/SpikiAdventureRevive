using UnityEngine;
using System.Collections;

public class ExplosionEffect : MonoBehaviour
{
    [Header("폭발 스프라이트")]
    public Sprite[] explosionSprites;

    [Header("프레임 속도")]
    public float frameRate = 0.05f;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        StartCoroutine(PlayExplosion());
    }

    IEnumerator PlayExplosion()
    {
        for (int i = 0; i < explosionSprites.Length; i++)
        {
            sr.sprite = explosionSprites[i];

            yield return new WaitForSeconds(frameRate);
        }

        Destroy(gameObject);
    }
}