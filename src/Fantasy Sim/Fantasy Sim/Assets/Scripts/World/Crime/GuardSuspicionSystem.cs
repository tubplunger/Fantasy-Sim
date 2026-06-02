using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSuspicionSystem : MonoBehaviour
{
    private int suspicionLevel = 0;

    private void OnEnable()
    {
        EventBus.Subscribe<NPCAttackedEvent>(OnNPCAttacked);
        EventBus.Subscribe<PlayerStoleItemEvent>(OnPlayerStoleItem);
        EventBus.Subscribe<FactionHostilityChangedEvent>(OnFactionHostilityChanged);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<NPCAttackedEvent>(OnNPCAttacked);
        EventBus.Unsubscribe<PlayerStoleItemEvent>(OnPlayerStoleItem);
        EventBus.Unsubscribe<FactionHostilityChangedEvent>(OnFactionHostilityChanged);
    }

    private void OnNPCAttacked(NPCAttackedEvent eventData)
    {
        IncreaseSuspicion(25, $"Player attacked {eventData.VictimName}");
    }

    private void OnPlayerStoleItem(PlayerStoleItemEvent eventData)
    {
        IncreaseSuspicion(10, $"Player stole {eventData.ItemName}");
    }

    private void OnFactionHostilityChanged(FactionHostilityChangedEvent eventData)
    {
        if (eventData.FactionId == "TownGuard" && eventData.NewValue >= 50)
        {
            IncreaseSuspicion(15, "Town Guard hostility became serious");
        }
    }

    private void IncreaseSuspicion(int amount, string reason)
    {
        int oldValue = suspicionLevel;
        int newValue = Mathf.Clamp(oldValue + amount, 0, 100);

        suspicionLevel = newValue;

        EventBus.Publish(new GuardSuspicionChangedEvent(
            oldValue,
            newValue,
            reason
        ));

        if (suspicionLevel >= 75)
        {
            Debug.Log("[GUARD BEHAVIOR] Guards are now actively watching the player.");
        }
        else if (suspicionLevel >= 40)
        {
            Debug.Log("[GUARD BEHAVIOR] Guards are suspicious of the player.");
        }
        else
        {
            Debug.Log("[GUARD BEHAVIOR] Guards are calm.");
        }
    }
}
