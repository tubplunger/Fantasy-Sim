using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEventLogger : MonoBehaviour
{
    private readonly List<IWorldEvent> eventHistory = new();

    private int eventCounter = 0;

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
        eventCounter++;
        eventHistory.Add(worldEvent);

        if (worldEvent is PlayerActionStartedEvent)
        {
            Debug.Log(
                $"\n<color=cyan><b>========== PLAYER ACTION #{eventCounter:000} ==========</b></color>\n" +
                $"<b>{worldEvent.Description}</b>"
            );

            return;
        }

        Debug.Log(
            $"<b>[{eventCounter:000}]</b> {worldEvent.EventName}\n" +
            $"<color=grey>{worldEvent.TimeStamp}</color>\n" +
            $"{worldEvent.Description}"
        );
    }

    public IReadOnlyList<IWorldEvent> GetEventHistory()
    {
        return eventHistory;
    }
}
