using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractionDemo : MonoBehaviour
{
    [SerializeField] private NPCMemorySystem memorySystem;
    [SerializeField] private RelationshipSystem relationshipSystem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("[DEMO INPUT] Player tries to buy from the baker.");

            TryBakerInteraction();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("[DEMO INPUT] Player talks to the guard.");

            TryGuardInteraction();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("[DEMO INPUT] Player talks to companion.");

            TryCompanionInteraction();
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            memorySystem.PrintMemoriesForNPC("npc_baker_001");
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            memorySystem.PrintMemoriesForNPC("npc_guard_001");
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            memorySystem.PrintMemoriesForNPC("npc_companion_001");
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            memorySystem.ClearAllMemories();
        }
    }

    private void TryBakerInteraction()
    {
        NPCMemory memory = memorySystem.GetStrongestMemoryOf(
            "npc_baker_001",
            MemoryType.Theft,
            "Player"
        );

        if (memory != null && Mathf.Abs(memory.GetCurrentStrength()) >= 25)
        {
            Debug.Log("[BAKER INTERACTION] Baker: I remember you stealing bread from me. No service.");
            return;
        }

        Debug.Log("[BAKER INTERACTION] Baker: What can I get for you?");
    }

    private void TryGuardInteraction()
    {
        NPCMemory memory = memorySystem.GetStrongestMemoryOf(
            "npc_guard_001",
            MemoryType.Theft,
            "Player"
        );

        if (memory != null && Mathf.Abs(memory.GetCurrentStrength()) >= 20)
        {
            Debug.Log("[GUARD INTERACTION] Guard: I heard about that bread theft. Keep your hands where I can see them.");
            return;
        }

        Debug.Log("[GUARD INTERACTION] Guard: Stay out of trouble.");
    }

    private void TryCompanionInteraction()
    {
        RelationshipState companionRelationship =
        relationshipSystem.GetRelationship("npc_companion_001", "Player");

        NPCMemory theftMemory = memorySystem.GetStrongestMemoryOf(
            "npc_companion_001",
            MemoryType.Theft,
            "Player"
        );

        NPCMemory violenceMemory = memorySystem.GetStrongestMemoryOf(
            "npc_companion_001",
            MemoryType.Violence,
            "Player"
        );

        NPCMemory betrayalMemory = memorySystem.GetStrongestMemoryOf(
            "npc_companion_001",
            MemoryType.Betrayal,
            "Player"
        );

        if (violenceMemory != null && Mathf.Abs(violenceMemory.GetCurrentStrength()) >= 30)
        {
            Debug.Log("[COMPANION INTERACTION] Companion: I do not like how easily you turn to violence.");
            return;
        }

        if (theftMemory != null && Mathf.Abs(theftMemory.GetCurrentStrength()) >= 15)
        {
            Debug.Log("[COMPANION INTERACTION] Companion: Stealing bread? Really? We need to be better than that.");
            return;
        }

        if (betrayalMemory != null && Mathf.Abs(betrayalMemory.GetCurrentStrength()) >= 30)
        {
            Debug.Log("[COMPANION INTERACTION] Companion: After what you did, I am not sure I can trust you.");
            return;
        }

        Debug.Log("[COMPANION INTERACTION] Companion: I'm with you.");
    }
}
