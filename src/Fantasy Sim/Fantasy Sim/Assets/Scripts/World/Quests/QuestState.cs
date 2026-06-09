using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class QuestState
{
    public string QuestId;
    public string Title;
    public string Description;
    public QuestType QuestType;
    public string OriginReason;
    public DateTime TimeGenerated;

    public QuestState(
        string questId,
        string title,
        string description,
        QuestType questType,
        string originReason)
    {
        QuestId = questId;
        Title = title;
        Description = description;
        QuestType = questType;
        OriginReason = originReason;
        TimeGenerated = DateTime.Now;
    }
}
