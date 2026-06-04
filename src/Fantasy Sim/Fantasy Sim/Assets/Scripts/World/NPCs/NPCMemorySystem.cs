using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMemorySystem : MonoBehaviour
{
    private readonly Dictionary<string, List<NPCMemory>> npcMemories = new();

    private void OnEnable()
    {
        EventBus.Subscribe<PlayerStoleItemEvent>(OnPlayerStoleItem);
        EventBus.Subscribe<NPCAttackedEvent>(OnNPCAttacked);
        EventBus.Subscribe<PlayerHelpedNPCEvent>(OnPlayerHelpedNPC);
        EventBus.Subscribe<PlayerBetrayedNPCEvent>(OnPlayerBetrayedNPC);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PlayerStoleItemEvent>(OnPlayerStoleItem);
        EventBus.Unsubscribe<NPCAttackedEvent>(OnNPCAttacked);
        EventBus.Unsubscribe<PlayerHelpedNPCEvent>(OnPlayerHelpedNPC);
        EventBus.Unsubscribe<PlayerBetrayedNPCEvent>(OnPlayerBetrayedNPC);
    }

    private void OnPlayerStoleItem(PlayerStoleItemEvent eventData)
    {
        // Baker directly remembers the bread theft.
        if (eventData.ItemName == "Bread")
        {
            AddMemory(
                "npc_baker_001",
                new NPCMemory(
                    MemoryType.Theft,
                    eventData.Description,
                    eventData.PlayerName,
                    "npc_baker_001",
                    -50,
                    0.5f
                )
            );
        }

        // Guard hears or witnesses the theft.
        AddMemory(
            "npc_guard_001",
            new NPCMemory(
                MemoryType.Theft,
                eventData.Description,
                eventData.PlayerName,
                eventData.OwnerFactionId,
                -35,
                0.25f
            )
        );

        // Companion remembers moral concern.
        AddMemory(
            "npc_companion_001",
            new NPCMemory(
                MemoryType.Theft,
                $"The player stole {eventData.ItemName}.",
                eventData.PlayerName,
                eventData.OwnerFactionId,
                -25,
                0.15f
            )
        );
    }

    private void OnNPCAttacked(NPCAttackedEvent eventData)
    {
        AddMemory(
            eventData.VictimNpcId,
            new NPCMemory(
                MemoryType.Violence,
                eventData.Description,
                eventData.AttackerName,
                eventData.VictimNpcId,
                -75,
                0.2f
            )
        );

        AddMemory(
            "npc_guard_001",
            new NPCMemory(
                MemoryType.Violence,
                eventData.Description,
                eventData.AttackerName,
                eventData.VictimNpcId,
                -60,
                0.15f
            )
        );

        AddMemory(
            "npc_companion_001",
            new NPCMemory(
                MemoryType.Violence,
                $"The player attacked {eventData.VictimName}.",
                eventData.AttackerName,
                eventData.VictimNpcId,
                -45,
                0.1f
            )
        );
    }

    private void OnPlayerHelpedNPC(PlayerHelpedNPCEvent eventData)
    {
        AddMemory(
            eventData.TargetNpcId,
            new NPCMemory(
                MemoryType.Kindness,
                eventData.Description,
                eventData.PlayerId,
                eventData.TargetNpcId,
                40,
                0.1f
            )
        );

        AddMemory(
            "npc_companion_001",
            new NPCMemory(
                MemoryType.Kindness,
                $"The player helped {eventData.TargetNpcName}.",
                eventData.PlayerId,
                eventData.TargetNpcId,
                25,
                0.1f
            )
        );
    }

    private void OnPlayerBetrayedNPC(PlayerBetrayedNPCEvent eventData)
    {
        AddMemory(
            eventData.TargetNpcId,
            new NPCMemory(
                MemoryType.Betrayal,
                eventData.Description,
                eventData.PlayerId,
                eventData.TargetNpcId,
                -90,
                0.05f
            )
        );

        AddMemory(
            "npc_companion_001",
            new NPCMemory(
                MemoryType.Betrayal,
                $"The player betrayed {eventData.TargetNpcName}.",
                eventData.PlayerId,
                eventData.TargetNpcId,
                -70,
                0.05f
            )
        );
    }

    private void AddMemory(string npcId, NPCMemory memory)
    {
        if (!npcMemories.ContainsKey(npcId))
        {
            npcMemories[npcId] = new List<NPCMemory>();
        }

        npcMemories[npcId].Add(memory);

        Debug.Log(
            $"[NPC MEMORY] {npcId} remembers {memory.MemoryType}: {memory.EventRemebered}\n" +
            $"Actor: {memory.ActorId} | Impact: {memory.EmotionalImpact} | Strength: {memory.GetCurrentStrength():0.0}"
        );
    }

    public bool HasStrongMemoryOf(string npcId, MemoryType memoryType, string actorId, float minimumStrength)
    {
        if (!npcMemories.ContainsKey(npcId))
            return false;

        foreach (NPCMemory memory in npcMemories[npcId])
        {
            if (memory.MemoryType != memoryType)
                continue;

            if (memory.ActorId != actorId)
                continue;

            if (Mathf.Abs(memory.GetCurrentStrength()) >= minimumStrength)
                return true;
        }

        return false;
    }

    public NPCMemory GetStrongestMemoryOf(string npcId, MemoryType memoryType, string actorId)
    {
        if (!npcMemories.ContainsKey(npcId))
            return null;

        NPCMemory strongestMemory = null;
        float strongestStrength = 0;

        foreach (NPCMemory memory in npcMemories[npcId])
        {
            if (memory.MemoryType != memoryType)
                continue;

            if (memory.ActorId != actorId)
                continue;

            float strength = Mathf.Abs(memory.GetCurrentStrength());

            if (strength > strongestStrength)
            {
                strongestStrength = strength;
                strongestMemory = memory;
            }
        }

        return strongestMemory;
    }

    public NPCMemory GetStrongestMemoryAboutActor(string npcId, string actorId)
    {
        if (!npcMemories.ContainsKey(npcId))
            return null;

        NPCMemory strongestMemory = null;
        float strongestStrength = 0;

        foreach (NPCMemory memory in npcMemories[npcId])
        {
            if (memory.ActorId != actorId)
                continue;

            float strength = Mathf.Abs(memory.GetCurrentStrength());

            if (strength > strongestStrength)
            {
                strongestStrength = strength;
                strongestMemory = memory;
            }
        }

        return strongestMemory;
    }

    public void ClearAllMemories()
    {
        npcMemories.Clear();
        Debug.Log("[MEMORY DEBUG] All NPC memories cleared.");
    }

    public void PrintMemoriesForNPC(string npcId)
    {
        if (!npcMemories.ContainsKey(npcId) || npcMemories[npcId].Count == 0)
        {
            Debug.Log($"[MEMORY DEBUG] {npcId} has no memories.");
            return;
        }

        Debug.Log($"[MEMORY DEBUG] Memories for {npcId}:");

        foreach (NPCMemory memory in npcMemories[npcId])
        {
            Debug.Log(
                $"- Type: {memory.MemoryType}\n" +
                $"  Event: {memory.EventRemebered}\n" +
                $"  Actor: {memory.ActorId}\n" +
                $"  Target: {memory.TargetId}\n" +
                $"  Impact: {memory.EmotionalImpact}\n" +
                $"  Current Strength: {memory.GetCurrentStrength():0.0}\n" +
                $"  Time: {memory.TimeStamp}"
            );
        }
    }
}
