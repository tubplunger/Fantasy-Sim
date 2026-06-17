using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationReplaySystem : MonoBehaviour
{
    [SerializeField] private SimulationTimeline simulationTimeline;

    public void ReplayAllEvents()
    {
        IReadOnlyList<TimelineEntry> entries = simulationTimeline.GetTimeline();

        if (entries.Count == 0)
        {
            Debug.Log("[REPLAY] No events to replay.");
            return;
        }

        Debug.Log("[REPLAY] Replaying full simulation timeline:");

        foreach (TimelineEntry entry in entries)
        {
            Debug.Log($"[REPLAY] {entry.ToDisplayString()}");
        }
    }

    public void ReplayRecentEvents(int count)
    {
        List<TimelineEntry> entries = simulationTimeline.GetRecentEntries(count);

        if (entries.Count == 0)
        {
            Debug.Log("[REPLAY] No recent events to replay.");
            return;
        }

        Debug.Log($"[REPLAY] Replaying last {entries.Count} events:");

        foreach (TimelineEntry entry in entries)
        {
            Debug.Log($"[REPLAY] {entry.ToDisplayString()}");
        }
    }
}
