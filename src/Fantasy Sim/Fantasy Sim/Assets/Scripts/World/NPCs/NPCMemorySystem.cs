using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMemorySystem : MonoBehaviour
{
    private readonly Dictionary<string, List<string>> npcMemories = new();

    private void OnEnable()
    {
        EventBus.Subscribe<CrimeWitnessedEvent>(OnCrimeWitnessed);
        EventBus.Subscribe<NPCAttackedEvent>(OnNPCAttacked);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<CrimeWitnessedEvent>(OnCrimeWitnessed);
        EventBus.Unsubscribe<NPCAttackedEvent>(OnNPCAttacked);
    }

    private void OnCrimeWitnessed(CrimeWitnessedEvent eventData)
    {
        Remember(eventData.WitnessNpcId, eventData.Description);
    }

    private void OnNPCAttacked(NPCAttackedEvent eventData)
    {
        Remember(eventData.VictimNpcId, $"I was attacked by {eventData.AttackerName}.");
    }

    private void Remember(string npcId, string memory)
    {
        if (!npcMemories.ContainsKey(npcId))
        {
            npcMemories[npcId] = new List<string>();
        }

        npcMemories[npcId].Add(memory);

        Debug.Log($"[NPC MEMORY] {npcId} remembers: {memory}");
    }
}
