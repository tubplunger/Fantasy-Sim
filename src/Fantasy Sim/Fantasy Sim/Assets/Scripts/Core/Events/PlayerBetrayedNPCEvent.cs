using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBetrayedNPCEvent : IWorldEvent
{
    public string PlayerId { get; private set; }
    public string TargetNpcId { get; private set; }
    public string TargetNpcName { get; private set; }
    public string BetrayalDescription { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Player Betrayed NPC";

    public string Description =>
        $"{PlayerId} betrayed {TargetNpcName}: {BetrayalDescription}";

    public PlayerBetrayedNPCEvent(string playerId, string targetNpcId, string targetNpcName, string betrayalDescription)
    {
        PlayerId = playerId;
        TargetNpcId = targetNpcId;
        TargetNpcName = targetNpcName;
        BetrayalDescription = betrayalDescription;
        TimeStamp = DateTime.Now;
    }
}
