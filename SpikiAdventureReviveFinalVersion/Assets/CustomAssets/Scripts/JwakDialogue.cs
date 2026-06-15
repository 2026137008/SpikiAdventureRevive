using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class JwakDialogue : MonoBehaviour, IPointerDownHandler
{
    public GameObject JwakShunJwak;
    public Text ScriptText_dialogue;
    public Text ScriptText_name;
    public string[] dialogue
        = { "....",
            "좍.",
            "좍좍좍좍좍좍좍좍좍좍좍좍좍좍.",
            "좍좍, 좍좍좍.... 좍좍좍좍좍좍좍좍?",
            "좍좍좍좍! 좍좍좍좍좍좍?!!",
            "....좍좍좍, 좍좍좍좍좍....",
            "좌아아아아아아악!!!!"
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

        if (dialogue_count == 7)
        {
            SceneManager.LoadScene("Stage3Second");
            dialogue_count = 7;
        }

        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

            typingCoroutine = StartCoroutine(TypeText(dialogue[dialogue_count]));

        if (dialogue_count == 0 || dialogue_count == 1 || dialogue_count == 2 || dialogue_count == 3 || dialogue_count == 4 || dialogue_count == 5 || dialogue_count == 6 || dialogue_count == 7)
        {
            JwakShunJwak.SetActive(true);
            ScriptText_name.text = "좍슝 좍";
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