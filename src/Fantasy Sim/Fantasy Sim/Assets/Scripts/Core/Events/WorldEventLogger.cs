using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEventLogger : MonoBehaviour
{
    private readonly List<IWorldEvent> eventHistory = new();

    private void OnEnable()
    {
        EventBus.Subscribe<IWorldEvent>(LogEvent);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<IWorldEvent>(LogEvent);
    }

    private void LogEvent(IWorldEvent worldEvent)
    {
        eventHistory.Add(worldEvent);

        Debug.Log(
            $"[WORLD EVENT] {worldEvent.EventName}\n" +
            $"Time: {worldEvent.TimeStamp}\n" +
            $"Description: {worldEvent.Description}"
        );
    }

    public IReadOnlyList<IWorldEvent> GetEventHistory()
    {
        return eventHistory;
    }
}
