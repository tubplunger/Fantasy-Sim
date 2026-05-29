using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCAttackedEvent : IWorldEvent
{
    public string AttackerName { get; private set; }
    public string VictimNpcId { get; private set; }
    public string VictimName { get; private set; }
    public string VictimFactionId { get; private set; }
    public int DamageAmount { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "NPC Attacked";

    public string Description =>
        $"{AttackerName} attacked {VictimName} for {DamageAmount} damage.";

    public NPCAttackedEvent(string attackerName, string victimNpcId, string victimName, string victimFactionId, int damageAmount)
    {
        AttackerName = attackerName;
        VictimNpcId = victimNpcId;
        VictimName = victimName;
        VictimFactionId = victimFactionId;
        DamageAmount = damageAmount;
        TimeStamp = DateTime.Now;
    }
}
