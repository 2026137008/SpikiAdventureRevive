using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DialogueSecond : MonoBehaviour, IPointerDownHandler
{
    public GameObject SpikiNormal;
    public GameObject SpikiNervous;
    public GameObject SpikiCrying;
    public GameObject SpikiThinking;
    public Text ScriptText_dialogue;
    public Text ScriptText_name;
    public string[] dialogue
        = { "....",
            "마왕의 부하, '미니좍'들이 있는 것이에요.",
            "그래도 다행히 주변에 딱 10마리만 보이니,",
            "빠르게 해치우는 거에요!"
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

        if (dialogue_count == 4)
        {
            SceneManager.LoadScene("Stage2");
            dialogue_count = 4;
        }

        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

            typingCoroutine = StartCoroutine(TypeText(dialogue[dialogue_count]));

        if (dialogue_count == 0 || dialogue_count == 1 || dialogue_count == 2)
        {
            SpikiNormal.SetActive(false);
            SpikiNervous.SetActive(false);
            SpikiCrying.SetActive(false);
            SpikiThinking.SetActive(true);
            ScriptText_name.text = "스핔이";
        }

        else if (dialogue_count == 3 || dialogue_count == 4)
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