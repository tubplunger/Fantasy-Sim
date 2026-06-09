using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BanditAttackEvent : IWorldEvent
{
    public string AttackerFactionId { get; private set; }
    public string TargetFactionId { get; private set; }
    public string LocationId { get; private set; }
    public string ResourceType { get; private set; }
    public int Severity { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Bandit Attack";

    public string Description =>
        $"{AttackerFactionId} attacked a {TargetFactionId} caravan near {LocationId}. " +
        $"Resource hit: {ResourceType}. Severity: {Severity}.";

    public BanditAttackEvent(
        string attackerFactionId,
        string targetFactionId,
        string locationId,
        string resourceType,
        int severity)
    {
        AttackerFactionId = attackerFactionId;
        TargetFactionId = targetFactionId;
        LocationId = locationId;
        ResourceType = resourceType;
        Severity = severity;
        TimeStamp = DateTime.Now;
    }
}
