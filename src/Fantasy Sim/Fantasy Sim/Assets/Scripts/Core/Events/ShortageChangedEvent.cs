using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShortageChangedEvent : IWorldEvent
{
    public string LocationId { get; private set; }
    public string ResourceType { get; private set; }
    public int OldShortageLevel { get; private set; }
    public int NewShortageLevel { get; private set; }
    public string Reason { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Shortage Changed";

    public string Description =>
        $"{ResourceType} shortage in {LocationId} changed from {OldShortageLevel} to {NewShortageLevel}. Reason: {Reason}";

    public ShortageChangedEvent(
        string locationId,
        string resourceType,
        int oldShortageLevel,
        int newShortageLevel,
        string reason)
    {
        LocationId = locationId;
        ResourceType = resourceType;
        OldShortageLevel = oldShortageLevel;
        NewShortageLevel = newShortageLevel;
        Reason = reason;
        TimeStamp = DateTime.Now;
    }
}
