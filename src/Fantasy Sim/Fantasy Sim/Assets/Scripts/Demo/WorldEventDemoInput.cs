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

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            EventBus.Publish(new PlayerActionStartedEvent("Steal Bread From Baker"));

            Debug.Log("[DEMO INPUT] Player steals bread from the baker.");

            EventBus.Publish(new PlayerStoleItemEvent(
                "Player",
                "Bread",
                "Baker's Stall",
                "MerchantsGuild"
            ));
        }

        if(Input.GetKeyDown(KeyCode.Alpha6))
{
            EventBus.Publish(new PlayerActionStartedEvent("Help Baker"));

            EventBus.Publish(new PlayerHelpedNPCEvent(
                "Player",
                "npc_baker_001",
                "Baker Tomas",
                "Carried flour sacks into the bakery"
            ));
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            EventBus.Publish(new PlayerActionStartedEvent("Betray Companion"));

            EventBus.Publish(new PlayerBetrayedNPCEvent(
                "Player",
                "npc_companion_001",
                "Companion Mira",
                "Promised to protect a villager, then abandoned them"
            ));
        }
    }
}