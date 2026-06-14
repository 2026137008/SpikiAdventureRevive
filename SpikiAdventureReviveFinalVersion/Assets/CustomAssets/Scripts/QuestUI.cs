using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public TMP_Text questText;

    void Update()
    {
        Quest quest = QuestManager.Instance.slimeQuest;

        if (!quest.isAccepted)
        {
            questText.text = "";
            return;
        }

        if (!quest.isCompleted)
        {
            questText.text =
                quest.questName + "\n" +
                quest.currentCount + " / " +
                quest.targetCount;
        }
        else
        {
            questText.text =
                "[완료!]\n" +
                quest.questName;
        }
    }
}