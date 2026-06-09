using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortageSystem : MonoBehaviour
{
    private readonly Dictionary<string, int> shortageLevels = new();

    private void OnEnable()
    {
        EventBus.Subscribe<BanditAttackEvent>(OnBanditAttack);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<BanditAttackEvent>(OnBanditAttack);
    }

    public void OnBanditAttack(BanditAttackEvent eventData)
    {
        if (eventData.ResourceType != "Food")
            return;

        string key = GetShortageKey(eventData.LocationId, eventData.ResourceType);

        int oldValue = GetShortageLevel(eventData.LocationId, eventData.ResourceType);
        int newValue = Mathf.Clamp(oldValue + eventData.Severity, 0, 100);

        shortageLevels[key] = newValue;

        EventBus.Publish(new ShortageChangedEvent(
            eventData.LocationId,
            eventData.ResourceType,
            oldValue,
            newValue,
            eventData.Description
        ));
    }

    public int GetShortageLevel(string locationId, string resourceType)
    {
        string key = GetShortageKey(locationId, resourceType);

        if (!shortageLevels.ContainsKey(key))
            shortageLevels[key] = 0;

        return shortageLevels[key];
    }

    public void PrintShortages()
    {
        if (shortageLevels.Count == 0)
        {
            Debug.Log("[SHORTAGE DEBUG] No shortages currently tracked.");
            return;
        }

        foreach (var shortage in shortageLevels)
        {
            Debug.Log($"[SHORTAGE DEBUG] {shortage.Key}: {shortage.Value}");
        }
    }

    private string GetShortageKey(string locationId, string resourceType)
    {
        return $"{locationId}_{resourceType}";
    }
}
