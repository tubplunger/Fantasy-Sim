using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class TimelineEntry
{
    public int SequenceNumber;
    public string EventName;
    public string Description;
    public string EventType;
    public DateTime TimeStamp;

    public TimelineEntry(int sequenceNumber, IWorldEvent worldEvent)
    {
        SequenceNumber = sequenceNumber;
        EventName = worldEvent.EventName;
        Description = worldEvent.Description;
        EventType = worldEvent.GetType().Name;
        TimeStamp = worldEvent.TimeStamp;
    }

    public string ToDisplayString()
    {
        return $"#{SequenceNumber:000} [{EventType}] {Description}";
    }
}
