using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionReputationSystem : MonoBehaviour
{
    private readonly Dictionary<string, FactionState> factions = new();

    private void Awake()
    {
        CreateTestFactions();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<NPCAttackedEvent>(OnNPCAttacked);
        EventBus.Subscribe<PlayerStoleItemEvent>(OnPlayerStoleItem);
        EventBus.Subscribe<PlayerSavedNPCEvent>(OnPlayerSavedNPC);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<NPCAttackedEvent>(OnNPCAttacked);
        EventBus.Unsubscribe<PlayerStoleItemEvent>(OnPlayerStoleItem);
        EventBus.Unsubscribe<PlayerSavedNPCEvent>(OnPlayerSavedNPC);
    }

    private void CreateTestFactions()
    {
        FactionState townGuard = new FactionState("TownGuard", "Town Guard");
        FactionState merchantsGuild = new FactionState("MerchantsGuild", "Merchants Guild");
        FactionState thievesCircle = new FactionState("ThievesCircle", "Thieves' Circle");

        townGuard.Allies.Add("MerchantsGuild");
        townGuard.Enemies.Add("ThievesCircle");

        merchantsGuild.Allies.Add("TownGuard");
        merchantsGuild.Enemies.Add("ThievesCircle");

        thievesCircle.Enemies.Add("TownGuard");
        thievesCircle.Enemies.Add("MerchantsGuild");

        factions[townGuard.FactionId] = townGuard;
        factions[merchantsGuild.FactionId] = merchantsGuild;
        factions[thievesCircle.FactionId] = thievesCircle;
    }

    private void OnNPCAttacked(NPCAttackedEvent eventData)
    {
        ChangeReputation(
            eventData.VictimFactionId,
            -25,
            $"Player attacked faction member {eventData.VictimName}"
        );

        ChangeHostility(
            eventData.VictimFactionId,
            30,
            $"Player attacked faction member {eventData.VictimName}"
        );

        ApplyAllianceReactions(
            eventData.VictimFactionId,
            $"Player attacked ally of faction"
        );
    }

    private void OnPlayerStoleItem(PlayerStoleItemEvent eventData)
    {
        ChangeReputation(
            eventData.OwnerFactionId,
            -10,
            $"Player stole {eventData.ItemName}"
        );

        ChangeHostility(
            eventData.OwnerFactionId,
            10,
            $"Player stole from faction"
        );

        ApplyAllianceReactions(
            eventData.OwnerFactionId,
            $"Player stole from ally of faction"
        );
    }

    private void OnPlayerSavedNPC(PlayerSavedNPCEvent eventData)
    {
        ChangeReputation(
            eventData.TargetFactionId,
            15,
            $"Player saved faction member {eventData.TargetNpcName}"
        );

        ChangeHostility(
            eventData.TargetFactionId,
            -10,
            $"Player saved faction member {eventData.TargetNpcName}"
        );

        ApplyPositiveAllianceReactions(
            eventData.TargetFactionId,
            "Player helped allied faction member"
        );
    }

    private void ApplyAllianceReactions(string harmedFactionId, string reason)
    {
        foreach (FactionState faction in factions.Values)
        {
            if (faction.Allies.Contains(harmedFactionId))
            {
                ChangeReputation(faction.FactionId, -10, reason);
                ChangeHostility(faction.FactionId, 10, reason);
            }

            if (faction.Enemies.Contains(harmedFactionId))
            {
                ChangeReputation(faction.FactionId, 5, $"Player harmed enemy faction {harmedFactionId}");
                ChangeHostility(faction.FactionId, -5, $"Player harmed enemy faction {harmedFactionId}");
            }
        }
    }

    private void ApplyPositiveAllianceReactions(string helpedFactionId, string reason)
    {
        foreach (FactionState faction in factions.Values)
        {
            if (faction.FactionId == helpedFactionId)
                continue;

            if (faction.Allies.Contains(helpedFactionId))
            {
                ChangeReputation(faction.FactionId, 5, reason);
                ChangeHostility(faction.FactionId, -5, reason);
            }

            if (faction.Enemies.Contains(helpedFactionId))
            {
                ChangeReputation(faction.FactionId, -5, $"Player helped enemy faction {helpedFactionId}");
                ChangeHostility(faction.FactionId, 5, $"Player helped enemy faction {helpedFactionId}");
            }
        }
    }

    private void ChangeReputation(string factionId, int amount, string reason)
    {
        EnsureFactionExists(factionId);

        FactionState faction = factions[factionId];

        int oldValue = faction.Reputation;
        int newValue = oldValue + amount;

        faction.Reputation = newValue;

        EventBus.Publish(new FactionReputationChangedEvent(
            factionId,
            oldValue,
            newValue,
            reason
        ));
    }

    private void ChangeHostility(string factionId, int amount, string reason)
    {
        EnsureFactionExists(factionId);

        FactionState faction = factions[factionId];

        int oldValue = faction.Hostility;
        int newValue = Mathf.Clamp(oldValue + amount, 0, 100);

        faction.Hostility = newValue;

        EventBus.Publish(new FactionHostilityChangedEvent(
            factionId,
            oldValue,
            newValue,
            reason
        ));
    }

    private void EnsureFactionExists(string factionId)
    {
        if (factions.ContainsKey(factionId))
            return;

        factions[factionId] = new FactionState(factionId, factionId);
    }
}