using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAIDemoInput : MonoBehaviour
{
    [SerializeField] private UtilityAISystem utilityAISystem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            EventBus.Publish(new PlayerActionStartedEvent("Evaluate NPC Utility AI"));
            utilityAISystem.EvaluateAllNPCs();
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            EventBus.Publish(new PlayerActionStartedEvent("Attack Baker"));

            EventBus.Publish(new NPCAttackedEvent(
                DemoIds.Player,
                DemoIds.Baker,
                "Baker Tomas",
                DemoIds.MerchantsGuild,
                10
            ));
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            utilityAISystem.PrintCurrentGoals();
        }
    }
}
