using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationObservabilityDemoInput : MonoBehaviour
{
    [SerializeField] private UtilityAISystem utilityAISystem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            RunObservabilityDemo();
        }
    }

    private void RunObservabilityDemo()
    {
        EventBus.Publish(new PlayerActionStartedEvent("Full Systems Observability Demo"));

        Debug.Log("[OBSERVABILITY DEMO] Starting systems-driven world event chain.");

        EventBus.Publish(new PlayerStoleItemEvent(
            DemoIds.Player,
            "Bread",
            "Baker's Stall",
            DemoIds.MerchantsGuild
        ));

        EventBus.Publish(new BanditAttackEvent(
            attackerFactionId: "Bandits",
            targetFactionId: DemoIds.MerchantsGuild,
            locationId: "Eastroad",
            resourceType: "Food",
            severity: 70
        ));

        EventBus.Publish(new NPCAttackedEvent(
            DemoIds.Player,
            DemoIds.Baker,
            "Baker Tomas",
            DemoIds.MerchantsGuild,
            10
        ));

        utilityAISystem.EvaluateAllNPCs();
    }
}
