using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FactionReputationChangedEvent : IWorldEvent
{
    public string FactionId { get; private set; }
    public int OldValue { get; private set; }
    public int NewValue { get; private set; }
    public string Reason { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Faction Reputation Changed";

    public string Description =>
        $"Reputation with {FactionId} changed from {OldValue} to {NewValue}. Reason: {Reason}";

    public FactionReputationChangedEvent(string factionId, int oldValue, int newValue, string reason)
    {
        FactionId = factionId;
        OldValue = oldValue;
        NewValue = newValue;
        Reason = reason;
        TimeStamp = DateTime.Now;
    }
}
