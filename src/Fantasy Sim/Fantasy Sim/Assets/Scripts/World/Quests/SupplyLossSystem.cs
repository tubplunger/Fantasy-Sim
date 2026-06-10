using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyLossSystem : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.Subscribe<BanditAttackEvent>(OnBanditAttack);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<BanditAttackEvent>(OnBanditAttack);
    }

    private void OnBanditAttack(BanditAttackEvent eventData)
    {
        int amountStolen = Mathf.Clamp(eventData.Severity * 2, 10, 100);

        EventBus.Publish(new SuppliesStolenEvent(
            eventData.LocationId,
            eventData.ResourceType,
            amountStolen,
            eventData.AttackerFactionId
        ));
    }
}
