using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FactionReputationChangeRequestedEvent : IWorldEvent
{
    public string FactionId { get; private set; }
    public int Amount { get; private set; }
    public string Reason { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Faction Reputation Change Requested";

    public string Description =>
        $"Request to change reputation with {FactionId} by {Amount}. Reason: {Reason}";

    public FactionReputationChangeRequestedEvent(string factionId, int amount, string reason)
    {
        FactionId = factionId;
        Amount = amount;
        Reason = reason;
        TimeStamp = DateTime.Now;
    }
}
