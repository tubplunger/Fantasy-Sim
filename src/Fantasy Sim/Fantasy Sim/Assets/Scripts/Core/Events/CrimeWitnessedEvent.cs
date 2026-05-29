using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CrimeWitnessedEvent : IWorldEvent
{
    public string WitnessNpcId { get; private set; }
    public string WitnessName { get; private set; }
    public string CrimeDescription { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Crime Witnessed";

    public string Description =>
        $"{WitnessName} witnessed a crime: {CrimeDescription}";

    public CrimeWitnessedEvent(string witnessNpcId, string witnessName, string crimeDescription)
    {
        WitnessNpcId = witnessNpcId;
        WitnessName = witnessName;
        CrimeDescription = crimeDescription;
        TimeStamp = DateTime.Now;
    }
}
