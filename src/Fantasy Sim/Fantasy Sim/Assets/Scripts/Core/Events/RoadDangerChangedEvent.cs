using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoadDangerChangedEvent : IWorldEvent
{
    public string LocationId { get; private set; }
    public int OldDanger { get; private set; }
    public int NewDanger { get; private set; }
    public string Reason { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Road Danger Changed";

    public string Description =>
        $"Road danger near {LocationId} changed from {OldDanger} to {NewDanger}. Reason: {Reason}";

    public RoadDangerChangedEvent(string locationId, int oldDanger, int newDanger, string reason)
    {
        LocationId = locationId;
        OldDanger = oldDanger;
        NewDanger = newDanger;
        Reason = reason;
        TimeStamp = DateTime.Now;
    }
}
