using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FactionConflictChangedEvent : IWorldEvent
{
    public string FactionAId { get; private set; }
    public string FactionBId { get; private set; }
    public int OldConflictLevel { get; private set; }
    public int NewConflictLevel { get; private set; }
    public string Reason { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Faction Conflict Changed";

    public string Description =>
        $"Conflict between {FactionAId} and {FactionBId} changed from {OldConflictLevel} to {NewConflictLevel}. Reason: {Reason}";

    public FactionConflictChangedEvent(
        string factionAId,
        string factionBId,
        int oldConflictLevel,
        int newConflictLevel,
        string reason)
    {
        FactionAId = factionAId;
        FactionBId = factionBId;
        OldConflictLevel = oldConflictLevel;
        NewConflictLevel = newConflictLevel;
        Reason = reason;
        TimeStamp = DateTime.Now;
    }
}
