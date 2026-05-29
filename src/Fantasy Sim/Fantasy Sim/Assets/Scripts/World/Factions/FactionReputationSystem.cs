using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionReputationSystem : MonoBehaviour
{
    private readonly Dictionary<string, int> factionReputation = new();

    private void Awake()
    {
        factionReputation["TownGuard"] = 0;
        factionReputation["MerchantsGuild"] = 0;
    }

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
        ChangeReputation(eventData.OwnerFactionId, -10, $"Player stole {eventData.ItemName}");
    }

    private void OnNPCAttacked(NPCAttackedEvent eventData)
    {
        ChangeReputation(eventData.VictimFactionId, -20, $"Player attacked {eventData.VictimName}");
    }

    private void ChangeReputation(string factionId, int amount, string reason)
    {
        if (!factionReputation.ContainsKey(factionId))
        {
            factionReputation[factionId] = 0;
        }

        int oldValue = factionReputation[factionId];
        int newValue = oldValue + amount;

        factionReputation[factionId] = newValue;

        EventBus.Publish(new FactionReputationChangedEvent(
            factionId,
            oldValue,
            newValue,
            reason
        ));
    }
}
