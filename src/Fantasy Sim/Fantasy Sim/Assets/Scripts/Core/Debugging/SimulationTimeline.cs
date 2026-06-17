using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationTimeline : MonoBehaviour
{
    private readonly List<TimelineEntry> timeline = new();

    private int sequenceCounter = 0;

    private void OnEnable()
    {
        EventBus.Subscribe<IWorldEvent>(RecordEvent);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<IWorldEvent>(RecordEvent);
    }

    private void RecordEvent(IWorldEvent worldEvent)
    {
        sequenceCounter++;

        TimelineEntry entry = new TimelineEntry(sequenceCounter, worldEvent);
        timeline.Add(entry);
    }

    public IReadOnlyList<TimelineEntry> GetTimeline()
    {
        return timeline;
    }

    public List<TimelineEntry> GetRecentEntries(int count)
    {
        int startIndex = Mathf.Max(0, timeline.Count - count);
        return timeline.GetRange(startIndex, timeline.Count - startIndex);
    }

    public void PrintTimeline()
    {
        if (timeline.Count == 0)
        {
            Debug.Log("[TIMELINE] No events recorded.");
            return;
        }

        Debug.Log("[TIMELINE] Full event timeline:");

        foreach (TimelineEntry entry in timeline)
        {
            Debug.Log(entry.ToDisplayString());
        }
    }

    public void PrintRecentTimeline(int count)
    {
        List<TimelineEntry> recentEntries = GetRecentEntries(count);

        if (recentEntries.Count == 0)
        {
            Debug.Log("[TIMELINE] No recent events.");
            return;
        }

        Debug.Log($"[TIMELINE] Last {recentEntries.Count} events:");

        foreach (TimelineEntry entry in recentEntries)
        {
            Debug.Log(entry.ToDisplayString());
        }
    }

    public void ClearTimeline()
    {
        timeline.Clear();
        sequenceCounter = 0;

        Debug.Log("[TIMELINE] Timeline cleared.");
    }
}
