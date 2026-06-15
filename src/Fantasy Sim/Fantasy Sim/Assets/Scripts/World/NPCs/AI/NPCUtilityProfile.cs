using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCUtilityProfile
{
    public string NpcId;
    public string DisplayName;
    public string FactionId;

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

        Fear = fear;
        Greed = greed;
        Loyalty = loyalty;
        Revenge = revenge;
        Survival = survival;
    }
}
