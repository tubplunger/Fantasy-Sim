using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEventDemoInput : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("[DEMO INPUT] Player steals from the Merchants Guild.");

            EventBus.Publish(new PlayerStoleItemEvent(
                "Player",
                "Silver Ring",
                "Market Stall",
                "MerchantsGuild"
            ));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("[DEMO INPUT] Player attacks a Town Guard member.");

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
            Debug.Log("[DEMO INPUT] Player attacks a Merchants Guild member.");

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
            Debug.Log("[DEMO INPUT] Player attacks a Thieves' Circle member.");

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