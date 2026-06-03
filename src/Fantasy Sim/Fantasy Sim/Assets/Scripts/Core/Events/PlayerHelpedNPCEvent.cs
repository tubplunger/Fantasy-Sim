using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHelpedNPCEvent : IWorldEvent
{
    public string PlayerId { get; private set; }
    public string TargetNpcId { get; private set; }
    public string TargetNpcName { get; private set; }
    public string HelpDescription { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Player Helped NPC";

    public string Description =>
        $"{PlayerId} helped {TargetNpcName}: {HelpDescription}";

    public PlayerHelpedNPCEvent(string playerId, string targetNpcId, string targetNpcName, string helpDescription)
    {
        PlayerId = playerId;
        TargetNpcId = targetNpcId;
        TargetNpcName = targetNpcName;
        HelpDescription = helpDescription;
        TimeStamp = DateTime.Now;
    }
}