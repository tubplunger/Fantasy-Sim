using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestFailedEvent : IWorldEvent
{
    public QuestState Quest { get; private set; }
    public string FailureReason { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Quest Failed";

    public string Description =>
        $"Quest failed: {Quest.Title}. Reason: {FailureReason}";

    public QuestFailedEvent(QuestState quest, string failureReason)
    {
        Quest = quest;
        FailureReason = failureReason;
        TimeStamp = DateTime.Now;
    }
}