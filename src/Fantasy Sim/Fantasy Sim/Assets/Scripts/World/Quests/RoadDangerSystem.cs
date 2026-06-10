using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadDangerSystem : MonoBehaviour
{
    private readonly Dictionary<string, int> roadDanger = new();

    private void OnEnable()
    {
        EventBus.Subscribe<BanditAttackEvent>(OnBanditAttack);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<BanditAttackEvent>(OnBanditAttack);
    }

    private void OnBanditAttack(BanditAttackEvent eventData)
    {
        int oldValue = GetRoadDanger(eventData.LocationId);
        int newValue = Mathf.Clamp(oldValue + eventData.Severity, 0, 100);

        roadDanger[eventData.LocationId] = newValue;

        EventBus.Publish(new RoadDangerChangedEvent(
            eventData.LocationId,
            oldValue,
            newValue,
            eventData.Description
        ));
    }

    public int GetRoadDanger(string locationId)
    {
        if (!roadDanger.ContainsKey(locationId)) 
            roadDanger[locationId] = 0;

        return roadDanger[locationId];
    }

    public void PrintRoadDanger()
    {
        if (roadDanger.Count == 0)
        {
            Debug.Log("[ROAD DANGER DEBUG] No road danger tracked.");
            return;
        }

        foreach (var entry in roadDanger)
        {
            Debug.Log($"[ROAD DANGER DEBUG] {entry.Key}: {entry.Value}");
        }
    }
}
