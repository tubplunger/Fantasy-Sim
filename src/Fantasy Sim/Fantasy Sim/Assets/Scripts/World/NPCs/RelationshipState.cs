using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RelationshipState
{
    public string NpcId;
    public string ActorId;

    public int Trust;
    public int Fear;
    public int Loyalty;
    public int Suspicion;

    public RelationshipState(string npcId, string actorId)
    {
        NpcId = npcId;
        ActorId = actorId;

        Trust = 0;
        Fear = 0;
        Loyalty = 0;
        Suspicion = 0;
    }

    public void ModifyTrust(int amount)
    {
        Trust = Mathf.Clamp(Trust + amount, -100, 100);
    }

    public void ModifyFear(int amount)
    {
        Fear = Mathf.Clamp(Fear + amount, 0, 100);
    }

    public void ModifyLoyalty(int amount)
    {
        Loyalty = Mathf.Clamp(Loyalty + amount, 0, 100);
    }

    public void ModifySuspicion(int amount)
    {
        Suspicion = Mathf.Clamp(Suspicion + amount, 0, 100);
    }
}
