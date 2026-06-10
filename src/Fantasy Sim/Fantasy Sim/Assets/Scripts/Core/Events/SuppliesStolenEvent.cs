using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SuppliesStolenEvent : IWorldEvent
{
    public string LocationId { get; private set; }
    public string StolenResourceType { get; private set; }
    public int AmountStolen { get; private set; }
    public string ResponsibleFactionId { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Supplies Stolen";

    public string Description =>
        $"{ResponsibleFactionId} stole {AmountStolen} units of {StolenResourceType} near {LocationId}.";

    public SuppliesStolenEvent(string locationId, string stolenResourceType, int amountStolen, string responsibleFactionId)
    {
        LocationId = locationId;
        StolenResourceType = stolenResourceType;
        AmountStolen = amountStolen;
        ResponsibleFactionId = responsibleFactionId;
        TimeStamp = DateTime.Now;
    }
}
