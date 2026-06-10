using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI; // UI를 제어하기 위해 필요합니다.
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image fadeImage; // 1번에서 만든 FadeImage를 넣을 곳

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 1.0f; // 페이드에 걸릴 시간 (초)

    private bool isTransitioning = false; // 중복 실행 방지용 플래그

    void Update()
    {
        // 이미 씬 전환 중이라면 입력을 무시합니다.
        if (isTransitioning) return;

        if (Keyboard.current != null && 
           (Keyboard.current.enterKey.wasPressedThisFrame || Keyboard.current.numpadEnterKey.wasPressedThisFrame))
        {
            StartCoroutine(FadeAndLoadScene("Stage1"));
        }
    }

    IEnumerator FadeAndLoadScene(string sceneName)
    {
        isTransitioning = true;

        // 1. Fade Out: 화면을 점점 검은색으로 만듭니다 (Alpha 0 -> 1)
        float timer = 0f;
        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Clamp01(timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        color.a = 1f;
        fadeImage.color = color;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
