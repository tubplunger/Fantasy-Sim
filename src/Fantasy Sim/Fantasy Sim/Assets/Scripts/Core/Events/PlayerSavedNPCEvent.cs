using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSavedNPCEvent : IWorldEvent
{
    public string PlayerId { get; private set; }
    public string TargetNpcId { get; private set; }
    public string TargetNpcName { get; private set; }
    public string TargetFactionId { get; private set; }
    public string SaveDescription { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Player Saved NPC";

    public string Description =>
        $"{PlayerId} saved {TargetNpcName}. {SaveDescription}";

    public PlayerSavedNPCEvent(
        string playerId,
        string targetNpcId,
        string targetNpcName,
        string targetFactionId,
        string saveDescription)
    {
        PlayerId = playerId;
        TargetNpcId = targetNpcId;
        TargetNpcName = targetNpcName;
        TargetFactionId = targetFactionId;
        SaveDescription = saveDescription;
        TimeStamp = DateTime.Now;
    }
}