using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RefugeesArrivedEvent : IWorldEvent
{
    public string LocationId { get; private set; }
    public int RefugeeCount { get; private set; }
    public string Cause { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Refugees Arrived";

    public string Description =>
        $"{RefugeeCount} refugees arrived at {LocationId}. Cause: {Cause}";

    public RefugeesArrivedEvent(string locationId, int refugeeCount, string cause)
    {
        LocationId = locationId;
        RefugeeCount = refugeeCount;
        Cause = cause;
        TimeStamp = DateTime.Now;
    }
}
