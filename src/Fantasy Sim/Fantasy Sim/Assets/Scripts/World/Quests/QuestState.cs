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
    public QuestStatus Status;
    public QuestImportance Importance;

    public string LocationId;
    public string PrimaryFactionId;
    public string OriginReason;

    public DateTime TimeGenerated;
    public float ExpirationHours;

    public QuestState(
        string questId,
        string title,
        string description,
        QuestType questType,
        QuestImportance importance,
        string locationId,
        string primaryFactionId,
        string originReason,
        float expirationHours)
    {
        QuestId = questId;
        Title = title;
        Description = description;
        QuestType = questType;
        Importance = importance;
        LocationId = locationId;
        PrimaryFactionId = primaryFactionId;
        OriginReason = originReason;
        ExpirationHours = expirationHours;

        Status = QuestStatus.Active;
        TimeGenerated = DateTime.Now;
    }

    public bool IsExpired()
    {
        if (Status != QuestStatus.Active)
            return false;

        double hoursPassed = (DateTime.Now - TimeGenerated).TotalHours;
        return hoursPassed >= ExpirationHours;
    }
}
