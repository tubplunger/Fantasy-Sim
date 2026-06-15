using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCDecisionMadeEvent : IWorldEvent
{
    public NPCUtilityDecision Decision { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "NPC Decision Made";

    public string Description =>
        $"{Decision.NpcName} chose {Decision.ChosenGoal} with score {Decision.Score:0.0}. Reason: {Decision.Reason}";

    public NPCDecisionMadeEvent(NPCUtilityDecision decision)
    {
        Decision = decision;
        TimeStamp = DateTime.Now;
    }
}
