using System;

[Serializable]
public class Quest
{
    public string questName;
    public string description;

    public int targetCount;
    public int currentCount;

    public bool isAccepted;
    public bool isCompleted;
}