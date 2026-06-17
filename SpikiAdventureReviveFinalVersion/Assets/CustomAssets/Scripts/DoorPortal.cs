using UnityEngine;

public class DoorPortal : MonoBehaviour
{
    public string nextSceneName;

    private bool playerInside = false;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.Z))
        {
            if (QuestManager.Instance.IsQuestCompleted())
            {
                FadeManager.Instance.LoadScene(nextSceneName);
            }
            else
            {
                Debug.Log("퀘스트를 완료해야 합니다!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}