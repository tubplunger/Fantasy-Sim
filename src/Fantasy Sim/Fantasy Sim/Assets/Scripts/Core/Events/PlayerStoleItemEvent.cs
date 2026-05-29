using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStoleItemEvent : IWorldEvent
{
    public string PlayerName {  get; private set; }
    public string ItemName { get; private set; }
    public string LocationName { get; private set; }
    public string OwnerFactionId { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Player Stole Item";

    public string Description => $"{PlayerName} stole {ItemName} from {LocationName}. Owner faction: {OwnerFactionId}.";

    public PlayerStoleItemEvent(string playerName, string itemName, string locationName, string ownerFactionId)
    {
        PlayerName = playerName;
        ItemName = itemName;
        LocationName = locationName;
        OwnerFactionId = ownerFactionId;
        TimeStamp = DateTime.Now;
    }
}
