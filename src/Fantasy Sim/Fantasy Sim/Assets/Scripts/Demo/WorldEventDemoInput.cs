using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEventDemoInput : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EventBus.Publish(new PlayerActionStartedEvent("Stole Item"));

            EventBus.Publish(new PlayerStoleItemEvent(
                "Player",
                "Silver Ring",
                "Market Stall",
                "MerchantsGuild"
            ));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EventBus.Publish(new PlayerActionStartedEvent("Attack Town Guard"));

            EventBus.Publish(new NPCAttackedEvent(
                "Player",
                "npc_guard_002",
                "Guard Toma",
                "TownGuard",
                15
            ));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EventBus.Publish(new PlayerActionStartedEvent("Attack Merchant"));

            EventBus.Publish(new NPCAttackedEvent(
                "Player",
                "npc_merchant_001",
                "Merchant Sela",
                "MerchantsGuild",
                10
            ));
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EventBus.Publish(new PlayerActionStartedEvent("Attack Thief"));

            EventBus.Publish(new NPCAttackedEvent(
                "Player",
                "npc_thief_001",
                "Varric the Knife",
                "ThievesCircle",
                20
            ));
        }
    }
}