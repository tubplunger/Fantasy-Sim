using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationshipSystem : MonoBehaviour
{
    private readonly Dictionary<string, RelationshipState> relationships = new();

    private void OnEnable()
    {
        EventBus.Subscribe<PlayerSavedNPCEvent>(OnPlayerSavedNPC);
        EventBus.Subscribe<PlayerHelpedNPCEvent>(OnPlayerHelpedNPC);
        EventBus.Subscribe<PlayerStoleItemEvent>(OnPlayerStoleItem);
        EventBus.Subscribe<NPCAttackedEvent>(OnNPCAttacked);
        EventBus.Subscribe<PlayerBetrayedNPCEvent>(OnPlayerBetrayedNPC);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PlayerSavedNPCEvent>(OnPlayerSavedNPC);
        EventBus.Unsubscribe<PlayerHelpedNPCEvent>(OnPlayerHelpedNPC);
        EventBus.Unsubscribe<PlayerStoleItemEvent>(OnPlayerStoleItem);
        EventBus.Unsubscribe<NPCAttackedEvent>(OnNPCAttacked);
        EventBus.Unsubscribe<PlayerBetrayedNPCEvent>(OnPlayerBetrayedNPC);
    }

    private void OnPlayerSavedNPC(PlayerSavedNPCEvent eventData)
    {
        ChangeRelationship(
            eventData.TargetNpcId,
            eventData.PlayerId,
            trustChange: 35,
            fearChange: -10,
            loyaltyChange: 25,
            suspicionChange: -15,
            reason: $"Player saved {eventData.TargetNpcName}"
        );

        ChangeRelationship(
            "npc_companion_001",
            eventData.PlayerId,
            trustChange: 15,
            fearChange: 0,
            loyaltyChange: 10,
            suspicionChange: -5,
            reason: $"Player saved {eventData.TargetNpcName}"
        );
    }

    private void OnPlayerHelpedNPC(PlayerHelpedNPCEvent eventData)
    {
        ChangeRelationship(
            eventData.TargetNpcId,
            eventData.PlayerId,
            trustChange: 20,
            fearChange: 0,
            loyaltyChange: 10,
            suspicionChange: -10,
            reason: eventData.HelpDescription
        );
    }

    private void OnPlayerStoleItem(PlayerStoleItemEvent eventData)
    {
        if (eventData.ItemName == "Bread")
        {
            ChangeRelationship(
                "npc_baker_001",
                eventData.PlayerName,
                trustChange: -35,
                fearChange: 0,
                loyaltyChange: -10,
                suspicionChange: 30,
                reason: $"Player stole {eventData.ItemName}"
            );
        }

        ChangeRelationship(
            "npc_companion_001",
            eventData.PlayerName,
            trustChange: -10,
            fearChange: 0,
            loyaltyChange: -5,
            suspicionChange: 10,
            reason: $"Player stole {eventData.ItemName}"
        );
    }

    private void OnNPCAttacked(NPCAttackedEvent eventData)
    {
        ChangeRelationship(
            eventData.VictimNpcId,
            eventData.AttackerName,
            trustChange: -50,
            fearChange: 40,
            loyaltyChange: -25,
            suspicionChange: 35,
            reason: $"Player attacked {eventData.VictimName}"
        );

        ChangeRelationship(
            "npc_companion_001",
            eventData.AttackerName,
            trustChange: -20,
            fearChange: 10,
            loyaltyChange: -10,
            suspicionChange: 15,
            reason: $"Player used violence against {eventData.VictimName}"
        );
    }

    private void OnPlayerBetrayedNPC(PlayerBetrayedNPCEvent eventData)
    {
        ChangeRelationship(
            eventData.TargetNpcId,
            eventData.PlayerId,
            trustChange: -80,
            fearChange: 15,
            loyaltyChange: -50,
            suspicionChange: 60,
            reason: eventData.BetrayalDescription
        );
    }

    private void ChangeRelationship(
        string npcId,
        string actorId,
        int trustChange,
        int fearChange,
        int loyaltyChange,
        int suspicionChange,
        string reason)
    {
        RelationshipState relationship = GetOrCreateRelationship(npcId, actorId);

        int oldTrust = relationship.Trust;
        int oldFear = relationship.Fear;
        int oldLoyalty = relationship.Loyalty;
        int oldSuspicion = relationship.Suspicion;

        relationship.ModifyTrust(trustChange);
        relationship.ModifyFear(fearChange);
        relationship.ModifyLoyalty(loyaltyChange);
        relationship.ModifySuspicion(suspicionChange);

        EventBus.Publish(new RelationshipChangedEvent(
            npcId,
            actorId,
            oldTrust,
            relationship.Trust,
            oldFear,
            relationship.Fear,
            oldLoyalty,
            relationship.Loyalty,
            oldSuspicion,
            relationship.Suspicion,
            reason
        ));
    }

    public RelationshipState GetRelationship(string npcId, string actorId)
    {
        return GetOrCreateRelationship(npcId, actorId);
    }

    public void PrintRelationship(string npcId, string actorId)
    {
        RelationshipState relationship = GetOrCreateRelationship(npcId, actorId);

        Debug.Log(
            $"[RELATIONSHIP DEBUG] {npcId} toward {actorId} | " +
            $"Trust: {relationship.Trust} | " +
            $"Fear: {relationship.Fear} | " +
            $"Loyalty: {relationship.Loyalty} | " +
            $"Suspicion: {relationship.Suspicion}"
        );
    }

    private RelationshipState GetOrCreateRelationship(string npcId, string actorId)
    {
        string key = GetRelationshipKey(npcId, actorId);

        if (!relationships.ContainsKey(key))
        {
            relationships[key] = new RelationshipState(npcId, actorId);
        }

        return relationships[key];
    }

    private string GetRelationshipKey(string npcId, string actorId)
    {
        return $"{npcId}_{actorId}";
    }
}
