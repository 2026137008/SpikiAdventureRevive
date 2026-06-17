using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    [Header("페이드 이미지")]
    public Image fadeImage;

    [Header("페이드 시간")]
    public float fadeTime = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        if (fadeImage == null)
        {
            Debug.LogError("FadeImage가 연결되지 않았습니다!");
            yield break;
        }

        Color color = fadeImage.color;

        color.a = 1f;
        fadeImage.color = color;

        float timer = fadeTime;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;

            color.a = timer / fadeTime;
            fadeImage.color = color;

            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
    }

    public void LoadScene(string sceneName)
    {
        if (fadeImage == null)
        {
            Debug.LogError("FadeImage가 연결되지 않았습니다!");
            return;
        }

        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        Color color = fadeImage.color;

        color.a = 0f;
        fadeImage.color = color;

        float timer = 0f;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;

            color.a = timer / fadeTime;
            fadeImage.color = color;

            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;

        SceneManager.LoadScene(sceneName);
    }
}