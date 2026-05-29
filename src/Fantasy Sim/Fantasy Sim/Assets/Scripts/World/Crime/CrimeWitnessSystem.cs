using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrimeWitnessSystem : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.Subscribe<PlayerStoleItemEvent>(OnPlayerStoleItem);
        EventBus.Subscribe<NPCAttackedEvent>(OnNPCAttacked);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PlayerStoleItemEvent>(OnPlayerStoleItem);
        EventBus.Unsubscribe<NPCAttackedEvent>(OnNPCAttacked);
    }

    private void OnPlayerStoleItem(PlayerStoleItemEvent eventData)
    {
        EventBus.Publish(new CrimeWitnessedEvent(
            "npc_guard_001",
            "Guard Ren",
            eventData.Description
        ));
    }

    private void OnNPCAttacked(NPCAttackedEvent eventData)
    {
        EventBus.Publish(new CrimeWitnessedEvent(
            "npc_guard_001",
            "Guard Ren",
            eventData.Description
        ));
    }
}
