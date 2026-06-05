using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationshipInteractionDemo : MonoBehaviour
{
    [SerializeField] private RelationshipSystem relationshipSystem;

    private const string PlayerId = "Player";
    private const string VillagerId = "npc_villager_001";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            EventBus.Publish(new PlayerActionStartedEvent("Save Villager"));

            Debug.Log("[DEMO INPUT] Player saves a villager from danger.");

            EventBus.Publish(new PlayerSavedNPCEvent(
                PlayerId,
                VillagerId,
                "Lena the Villager",
                "TownGuard",
                "The player protected Lena from a wolf attack."
            ));
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            TalkToVillager();
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            relationshipSystem.PrintRelationship(VillagerId, PlayerId);
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            relationshipSystem.PrintRelationship("npc_companion_001", PlayerId);
        }
    }

    private void TalkToVillager()
    {
        RelationshipState relationship = relationshipSystem.GetRelationship(VillagerId, PlayerId);

        Debug.Log("[DEMO INPUT] Player talks to Lena the Villager.");

        if (relationship.Fear >= 50)
        {
            Debug.Log("[VILLAGER DIALOGUE] Lena: Please... just leave me alone.");
            return;
        }

        if (relationship.Suspicion >= 50)
        {
            Debug.Log("[VILLAGER DIALOGUE] Lena: I have heard troubling things about you.");
            return;
        }

        if (relationship.Trust >= 30 && relationship.Loyalty >= 20)
        {
            Debug.Log("[VILLAGER DIALOGUE] Lena: You saved my life. If you need help in this town, ask me.");
            return;
        }

        if (relationship.Trust >= 10)
        {
            Debug.Log("[VILLAGER DIALOGUE] Lena: I think you are a decent person.");
            return;
        }

        Debug.Log("[VILLAGER DIALOGUE] Lena: Sorry, do I know you?");
    }
}
