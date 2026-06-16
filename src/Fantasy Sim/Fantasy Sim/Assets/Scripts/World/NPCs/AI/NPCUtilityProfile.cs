using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCUtilityProfile
{
    public string NpcId;
    public string DisplayName;
    public string FactionId;
    public NPCGoalType CurrentGoal;
    public float CurrentGoalScore;

    [Range(0, 100)] public int Fear;
    [Range(0, 100)] public int Greed;
    [Range(0, 100)] public int Loyalty;
    [Range(0, 100)] public int Revenge;
    [Range(0, 100)] public int Survival;

    public NPCUtilityProfile(
        string npcId,
        string displayName,
        string factionId,
        int fear,
        int greed,
        int loyalty,
        int revenge,
        int survival)
    {
        NpcId = npcId;
        DisplayName = displayName;
        FactionId = factionId;
        CurrentGoal = NPCGoalType.StayCalm;
        CurrentGoalScore = 0;

        Fear = fear;
        Greed = greed;
        Loyalty = loyalty;
        Revenge = revenge;
        Survival = survival;
    }
}
