using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DialogueStart : MonoBehaviour, IPointerDownHandler
{
    public GameObject SpikiNormal;
    public GameObject SpikiNervous;
    public GameObject SpikiCrying;
    public GameObject SpikiThinking;
    public Text ScriptText_dialogue;
    public Text ScriptText_name;
    public string[] dialogue
        = { " ",
            "흐에에에엥!!!!",
            "큰일난 것이에요!",
            "마을의 보물인 전설의 호박을 마왕에게 뺏겼어요!",
            "저는 이제 마왕성으로 가는 거에요.",
            "이번에야말로 이 순환을 끝내는 거에요!!!!"
        };
    public int dialogue_count = 0;

    public AudioSource audioSource;
    public AudioClip textSound;

    public float typingSpeed = 0.05f;

    private Coroutine typingCoroutine;
    private bool isTyping = false;
    public void OnPointerDown(PointerEventData data)
    {

        if(isTyping)
        {
            StopCoroutine(typingCoroutine);

            ScriptText_dialogue.text = dialogue[dialogue_count];

            isTyping = false;
            return;
        }

        dialogue_count++;
        Debug.Log(dialogue_count);

        if (dialogue_count == 6)
        {
            SceneManager.LoadScene("Stage1");
            dialogue_count = 6;
        }

        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

            typingCoroutine = StartCoroutine(TypeText(dialogue[dialogue_count]));

        if (dialogue_count == 1 || dialogue_count == 2 || dialogue_count == 3)
        {
            SpikiNormal.SetActive(false);
            SpikiNervous.SetActive(false);
            SpikiCrying.SetActive(true);
            SpikiThinking.SetActive(false);
            ScriptText_name.text = "스핔이";
        }

        else if (dialogue_count == 4)
        {
            SpikiNormal.SetActive(false);
            SpikiNervous.SetActive(false);
            SpikiCrying.SetActive(false);
            SpikiThinking.SetActive(true);
            ScriptText_name.text = "스핔이";
        }

        else if (dialogue_count == 0 || dialogue_count == 5 || dialogue_count == 6)
        {
            SpikiNormal.SetActive(true);
            SpikiNervous.SetActive(false);
            SpikiCrying.SetActive(false);
            SpikiThinking.SetActive(false);
            ScriptText_name.text = "스핔이";
        }
    }

    IEnumerator TypeText(string sentence)
    {
        isTyping = true;

        ScriptText_dialogue.text = "";

        foreach(char letter in sentence)
        {
            ScriptText_dialogue.text += letter;

            int count = 0;

            if(letter != ' ')
            {
                audioSource.PlayOneShot(textSound);
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}