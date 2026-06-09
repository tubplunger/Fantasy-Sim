using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MigrationSystem : MonoBehaviour
{
    private readonly Dictionary<string, int> refugeeCounts = new();

    private void OnEnable()
    {
        EventBus.Subscribe<ShortageChangedEvent>(OnShortageChanged);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<ShortageChangedEvent>(OnShortageChanged);
    }

    private void OnShortageChanged(ShortageChangedEvent eventData)
    {
        if (eventData.ResourceType != "Food")
            return;

        if (eventData.NewShortageLevel < 30)
            return;

        int refugeesToAdd = Mathf.Clamp(eventData.NewShortageLevel / 2, 5, 50);

        AddRefugees(
            eventData.LocationId,
            refugeesToAdd,
            $"Food shortage reached {eventData.NewShortageLevel}"
        );
    }

    private void AddRefugees(string locationId, int amount, string cause)
    {
        if (!refugeeCounts.ContainsKey(locationId))
            refugeeCounts[locationId] = 0;

        refugeeCounts[locationId] += amount;

        EventBus.Publish(new RefugeesArrivedEvent(
            locationId,
            amount,
            cause
        ));
    }

    public int GetRefugeeCount(string locationId)
    {
        if (!refugeeCounts.ContainsKey(locationId))
            refugeeCounts[locationId] = 0;

        return refugeeCounts[locationId];
    }

    public void PrintRefugees()
    {
        if (refugeeCounts.Count == 0)
        {
            Debug.Log("[MIGRATION DEBUG] No refugees currently tracked.");
            return;
        }

        foreach (var refugeeGroup in refugeeCounts)
        {
            Debug.Log($"[MIGRATION DEBUG] {refugeeGroup.Key}: {refugeeGroup.Value} refugees");
        }
    }
}
