using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionConflictSystem : MonoBehaviour
{
    private readonly Dictionary<string, int> conflictLevels = new();

    private void OnEnable()
    {
        EventBus.Subscribe<BanditAttackEvent>(OnBanditAttack);
        EventBus.Subscribe<FactionHostilityChangedEvent>(OnFactionHostilityChanged);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<BanditAttackEvent>(OnBanditAttack);
        EventBus.Unsubscribe<FactionHostilityChangedEvent>(OnFactionHostilityChanged);
    }

    private void OnBanditAttack(BanditAttackEvent eventData)
    {
        IncreaseConflict(
            eventData.AttackerFactionId,
            eventData.TargetFactionId,
            eventData.Severity,
            eventData.Description
        );
    }

    private void OnFactionHostilityChanged(FactionHostilityChangedEvent eventData)
    {
        if (eventData.NewValue < 60)
            return;

        IncreaseConflict(
            "Player",
            eventData.FactionId,
            15,
            $"High hostility with {eventData.FactionId}"
        );
    }

    private void IncreaseConflict(string factionAId, string factionBId, int amount, string reason)
    {
        string key = GetConflictKey(factionAId, factionBId);

        int oldValue = GetConflictLevel(factionAId, factionBId);
        int newValue = Mathf.Clamp(oldValue + amount, 0, 100);

        conflictLevels[key] = newValue;

        EventBus.Publish(new FactionConflictChangedEvent(
            factionAId,
            factionBId,
            oldValue,
            newValue,
            reason
        ));
    }

    public int GetConflictLevel(string factionAId, string factionBId)
    {
        string key = GetConflictKey(factionAId, factionBId);

        if (!conflictLevels.ContainsKey(key))
            conflictLevels[key] = 0;

        return conflictLevels[key];
    }

    public void PrintConflicts()
    {
        if (conflictLevels.Count == 0)
        {
            Debug.Log("[CONFLICT DEBUG] No faction conflicts tracked.");
            return;
        }

        foreach (var entry in conflictLevels)
        {
            Debug.Log($"[CONFLICT DEBUG] {entry.Key}: {entry.Value}");
        }
    }

    private string GetConflictKey(string factionAId, string factionBId)
    {
        if (string.CompareOrdinal(factionAId, factionBId) < 0)
            return $"{factionAId}_{factionBId}";

        return $"{factionBId}_{factionAId}";
    }
}
