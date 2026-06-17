using System;

[Serializable]
public class Quest
{
    public string questName;

    public int currentCount;
    public int targetCount = 5;

    public bool isAccepted;
    public bool isCompleted;
}