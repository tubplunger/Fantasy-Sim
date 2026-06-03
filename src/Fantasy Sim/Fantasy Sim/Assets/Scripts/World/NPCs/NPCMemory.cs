using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class NPCMemory
{
    public MemoryType MemoryType;
    public string EventRemebered;
    public string ActorId;
    public string TargetId;
    public int EmotionalImpact;
    public DateTime TimeStamp;
    public float DecayFactor;

    public NPCMemory(MemoryType memoryType, string eventRemembered, string actorId, string targetId, int emotionalImpact, float decayFactor)
    {
        MemoryType = memoryType;
        EventRemebered = eventRemembered;
        ActorId = actorId;
        TargetId = targetId;
        EmotionalImpact = emotionalImpact;
        DecayFactor = decayFactor;
        TimeStamp = DateTime.Now;
    }

    public float GetCurrentStrength()
    {
        double hoursPassed = (DateTime.Now - TimeStamp).TotalHours;
        float decayedAmount = (float)hoursPassed * DecayFactor;

        if (EmotionalImpact > 0)
        {
            return MathF.Max(0, EmotionalImpact - decayedAmount);
        }

        if (EmotionalImpact < 0)
        {
            return MathF.Min(0, EmotionalImpact + decayedAmount);
        }

        return 0;
    }
}
