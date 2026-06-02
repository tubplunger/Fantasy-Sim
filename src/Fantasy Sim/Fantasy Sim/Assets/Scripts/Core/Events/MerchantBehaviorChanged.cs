using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MerchantBehaviorChanged : IWorldEvent
{
    public string MerchantFactionId { get; private set; }
    public string OldBehavior {  get; private set; }
    public string NewBehavior { get; private set; }
    public string Reason { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public string EventName => "Merchant Behavior changed.";

    public string Description => $"{MerchantFactionId} merchant behavior changed from {OldBehavior} to {NewBehavior}. Reason: {Reason}";

    public MerchantBehaviorChanged(string merchantFactionId, string oldBehavior, string newBehavior, string reason)
    {
        MerchantFactionId = merchantFactionId;
        OldBehavior = oldBehavior;
        NewBehavior = newBehavior;
        Reason = reason;
        TimeStamp = DateTime.Now;
    }
}
