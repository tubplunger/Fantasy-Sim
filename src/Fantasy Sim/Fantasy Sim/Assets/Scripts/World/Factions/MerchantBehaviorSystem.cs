using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantBehaviorSystem : MonoBehaviour
{
    private string currentBehavior = "Friendly";

    private void OnEnable()
    {
        EventBus.Subscribe<FactionReputationChangedEvent>(OnFactionReputationChanged);
        EventBus.Subscribe<FactionHostilityChangedEvent>(OnFactionHostilityChanged);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<FactionReputationChangedEvent>(OnFactionReputationChanged);
        EventBus.Unsubscribe<FactionHostilityChangedEvent>(OnFactionHostilityChanged);
    }

    private void OnFactionReputationChanged(FactionReputationChangedEvent eventData)
    {
        if (eventData.FactionId != "MerchantsGuild")
            return;

        if (eventData.NewValue <= -30)
        {
            ChangeBehavior("Refuses Service", "Reputation with Merchants Guild is too low");
        }
        else if (eventData.NewValue <= -10)
        {
            ChangeBehavior("Raises Prices", "Merchants distrust the player");
        }
        else
        {
            ChangeBehavior("Friendly", "Reputation is acceptable");
        }
    }

    private void OnFactionHostilityChanged(FactionHostilityChangedEvent eventData)
    {
        if (eventData.FactionId != "MerchantsGuild")
            return;

        if (eventData.NewValue >= 50)
        {
            ChangeBehavior("Calls Guards", "Merchants Guild hostility is high");
        }
    }

    private void ChangeBehavior(string newBehavior, string reason)
    {
        if (currentBehavior == newBehavior)
            return;

        string oldBehavior = currentBehavior;
        currentBehavior = newBehavior;

        EventBus.Publish(new MerchantBehaviorChanged(
            "MerchantsGuild",
            oldBehavior,
            newBehavior,
            reason
        ));

        Debug.Log($"[MERCHANT BEHAVIOR] Merchants now: {newBehavior}");
    }
}
