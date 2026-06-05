using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RelationshipChangedEvent : IWorldEvent
{
    public string NpcId { get; private set; }
    public string ActorId { get; private set; }

    public int OldTrust { get; private set; }
    public int NewTrust { get; private set; }

    public int OldFear { get; private set; }
    public int NewFear { get; private set; }

    public int OldLoyalty { get; private set; }
    public int NewLoyalty { get; private set; }

    public int OldSuspicion { get; private set; }
    public int NewSuspicion { get; private set; }

    public string Reason { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Relationship Changed";

    public string Description =>
        $"{NpcId}'s relationship with {ActorId} changed. " +
        $"Trust {OldTrust}->{NewTrust}, Fear {OldFear}->{NewFear}, " +
        $"Loyalty {OldLoyalty}->{NewLoyalty}, Suspicion {OldSuspicion}->{NewSuspicion}. " +
        $"Reason: {Reason}";

    public RelationshipChangedEvent(
        string npcId,
        string actorId,
        int oldTrust,
        int newTrust,
        int oldFear,
        int newFear,
        int oldLoyalty,
        int newLoyalty,
        int oldSuspicion,
        int newSuspicion,
        string reason)
    {
        NpcId = npcId;
        ActorId = actorId;

        OldTrust = oldTrust;
        NewTrust = newTrust;

        OldFear = oldFear;
        NewFear = newFear;

        OldLoyalty = oldLoyalty;
        NewLoyalty = newLoyalty;

        OldSuspicion = oldSuspicion;
        NewSuspicion = newSuspicion;

        Reason = reason;
        TimeStamp = DateTime.Now;
    }
}