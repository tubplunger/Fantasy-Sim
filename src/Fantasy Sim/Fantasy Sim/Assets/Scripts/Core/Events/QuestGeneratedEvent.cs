using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestGeneratedEvent : IWorldEvent
{
    public QuestState Quest { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Quest Generated";

    public string Description =>
        $"Quest generated: {Quest.Title}. Reason: {Quest.OriginReason}";

    public QuestGeneratedEvent(QuestState quest)
    {
        Quest = quest;
        TimeStamp = DateTime.Now;
    }
}
