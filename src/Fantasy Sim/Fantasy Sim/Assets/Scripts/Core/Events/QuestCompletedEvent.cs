using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestCompletedEvent : IWorldEvent
{
    public QuestState Quest { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Quest Completed";

    public string Description =>
        $"Quest completed: {Quest.Title}. Type: {Quest.QuestType}. Location: {Quest.LocationId}.";

    public QuestCompletedEvent(QuestState quest)
    {
        Quest = quest;
        TimeStamp = DateTime.Now;
    }
}
