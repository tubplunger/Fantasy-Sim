using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCUtilityDecision
{
    public string NpcId;
    public string NpcName;
    public NPCGoalType ChosenGoal;
    public float Score;
    public string Reason;

    public NPCUtilityDecision(
        string npcId,
        string npcName,
        NPCGoalType chosenGoal,
        float score,
        string reason)
    {
        NpcId = npcId;
        NpcName = npcName;
        ChosenGoal = chosenGoal;
        Score = score;
        Reason = reason;
    }
}
