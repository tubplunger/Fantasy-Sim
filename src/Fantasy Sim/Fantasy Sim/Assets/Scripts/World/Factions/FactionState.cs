using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FactionState
{
    public string FactionId;
    public string DisplayName;

    public int Reputation;
    public int Hostility;

    public List<string> Allies = new();
    public List<string> Enemies = new();

    public FactionState(string factionId, string displayName)
    {
        FactionId = factionId;
        DisplayName = displayName;
        Reputation = 0;
        Hostility = 0; 
    }
}
