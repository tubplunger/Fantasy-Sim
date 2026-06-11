using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestExpiredEvent : IWorldEvent
{
    public QuestState Quest { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Quest Expired";

    public string Description =>
        $"Quest expired: {Quest.Title}. The world moved on without player action.";

    public QuestExpiredEvent(QuestState quest)
    {
        Quest = quest;
        TimeStamp = DateTime.Now;
    }
}
