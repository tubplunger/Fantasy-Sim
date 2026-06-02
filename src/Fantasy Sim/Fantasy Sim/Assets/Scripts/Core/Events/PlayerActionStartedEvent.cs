using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerActionStartedEvent : IWorldEvent
{
    public string ActionName { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Player Action Started";

    public string Description => $"Player action began: {ActionName}";

    public PlayerActionStartedEvent(string actionName)
    {
        ActionName = actionName;
        TimeStamp = DateTime.Now;
    }
}
