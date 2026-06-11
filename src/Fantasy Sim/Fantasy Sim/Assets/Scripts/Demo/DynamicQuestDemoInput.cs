using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicQuestDemoInput : MonoBehaviour
{
    [SerializeField] private ShortageSystem shortageSystem;
    [SerializeField] private MigrationSystem migrationSystem;
    [SerializeField] private DynamicQuestSystems dynamicQuestSystem;
    [SerializeField] private RoadDangerSystem roadDangerSystem;
    [SerializeField] private FactionConflictSystem factionConflictSystem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            EventBus.Publish(new PlayerActionStartedEvent("Bandits Attack Food Caravan"));

            Debug.Log("[DEMO INPUT] Bandits attack a food caravan near Eastroad.");

            EventBus.Publish(new BanditAttackEvent(
                attackerFactionId: "Bandits",
                targetFactionId: DemoIds.MerchantsGuild,
                locationId: "Eastroad",
                resourceType: "Food",
                severity: 35
            ));
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            EventBus.Publish(new PlayerActionStartedEvent("Major Bandit Raid"));

            Debug.Log("[DEMO INPUT] Bandits launch a major raid near Eastroad.");

            EventBus.Publish(new BanditAttackEvent(
                attackerFactionId: "Bandits",
                targetFactionId: DemoIds.MerchantsGuild,
                locationId: "Eastroad",
                resourceType: "Food",
                severity: 70
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

        if (Input.GetKeyDown(KeyCode.F11))
        {
            roadDangerSystem.PrintRoadDanger();
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            factionConflictSystem.PrintConflicts();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            dynamicQuestSystem.ClearQuests();
        }

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            dynamicQuestSystem.CompleteFirstActiveQuest();
        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            dynamicQuestSystem.FailFirstActiveQuest();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            dynamicQuestSystem.PrintAllQuests();
        }
    }
}
