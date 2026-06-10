using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1.0f;

    void Start()
    {
        // 시작하자마자 검은색 화면에서 투명하게 만드는 코루틴 실행
        StartCoroutine(StartFadeIn());
    }

    IEnumerator StartFadeIn()
    {
        float timer = fadeDuration;
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            color.a = Mathf.Clamp01(timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        
        // 페이드가 끝나면 클릭 방지를 위해 이미지를 비활성화합니다.
        fadeImage.gameObject.SetActive(false); 
    }
}
