using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public Quest slimeQuest;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AcceptQuest();
    }

    public void AcceptQuest()
    {
        slimeQuest.isAccepted = true;

        Debug.Log("퀘스트 수락!");
    }

    public void AddProgress()
    {
        if (!slimeQuest.isAccepted)
            return;

        if (slimeQuest.isCompleted)
            return;

        slimeQuest.currentCount++;

        Debug.Log(
            "퀘스트 진행도 : "
            + slimeQuest.currentCount
            + " / "
            + slimeQuest.targetCount);

        if (slimeQuest.currentCount >= slimeQuest.targetCount)
        {
            slimeQuest.currentCount = slimeQuest.targetCount;
            slimeQuest.isCompleted = true;

            Debug.Log("퀘스트 완료!");
        }
    }

    public bool IsQuestCompleted()
    {
        return slimeQuest.isCompleted;
    }
}