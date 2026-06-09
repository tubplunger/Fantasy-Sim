using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicQuestDemoInput : MonoBehaviour
{
    [SerializeField] private ShortageSystem shortageSystem;
    [SerializeField] private MigrationSystem migrationSystem;
    [SerializeField] private DynamicQuestSystems dynamicQuestSystem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            EventBus.Publish(new PlayerActionStartedEvent("Bandits Attack Caravan"));

            Debug.Log("[DEMO INPUT] Bandits attack a food caravan near Eastroad.");

            EventBus.Publish(new BanditAttackEvent(
                attackerFactionId: "Bandits",
                targetFactionId: DemoIds.MerchantsGuild,
                locationId: "Eastroad",
                resourceType: "Food",
                severity: 35
            ));
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            dynamicQuestSystem.PrintActiveQuests();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            shortageSystem.PrintShortages();
        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            migrationSystem.PrintRefugees();
        }
    }
}
