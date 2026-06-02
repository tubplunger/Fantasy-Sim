using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GuardSuspicionChangedEvent : IWorldEvent
{
    public int OldValue { get; private set; }
    public int NewValue { get; private set; }
    public string Reason { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Guard Suspicion changed.";

    public string Description => $"Guard suspicion changed from {OldValue} to {NewValue}. Reason: {Reason}";

    public GuardSuspicionChangedEvent(int oldValue, int newValue, string reason)
    {
        OldValue = oldValue;
        NewValue = newValue;
        Reason = reason;
        TimeStamp = DateTime.Now;
    }
}
