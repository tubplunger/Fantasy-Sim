using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEventDemoInput : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EventBus.Publish(new PlayerStoleItemEvent(
                "Player",
                "Silver Ring",
                "Market Stall",
                "MerchantsGuild"
            ));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EventBus.Publish(new NPCAttackedEvent(
                "Player",
                "npc_citizen_001",
                "Toma the Baker",
                "TownGuard",
                15
            ));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EventBus.Publish(new CrimeWitnessedEvent(
                "npc_farmer_001",
                "Mira the Farmer",
                "The player was seen sneaking near a locked chest."
            ));
        }
    }
}