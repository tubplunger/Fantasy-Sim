using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicQuestSystems : MonoBehaviour
{
    private readonly List<QuestState> quests = new();
    private readonly HashSet<string> generatedQuestKeys = new();

    private void Update()
    {
        CheckForExpiredQuests();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<RefugeesArrivedEvent>(OnRefugeesArrived);
        EventBus.Subscribe<SuppliesStolenEvent>(OnSuppliesStolen);
        EventBus.Subscribe<RoadDangerChangedEvent>(OnRoadDangerChanged);
        EventBus.Subscribe<FactionConflictChangedEvent>(OnFactionConflictChanged);
        EventBus.Subscribe<ShortageChangedEvent>(OnShortageChanged);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<RefugeesArrivedEvent>(OnRefugeesArrived);
        EventBus.Unsubscribe<SuppliesStolenEvent>(OnSuppliesStolen);
        EventBus.Unsubscribe<RoadDangerChangedEvent>(OnRoadDangerChanged);
        EventBus.Unsubscribe<FactionConflictChangedEvent>(OnFactionConflictChanged);
        EventBus.Unsubscribe<ShortageChangedEvent>(OnShortageChanged);
    }

    private void OnRefugeesArrived(RefugeesArrivedEvent eventData)
    {
        if (eventData.RefugeeCount >= 10)
        {
            TryGenerateQuest(
                questKey: $"Protection_Refugees_{eventData.LocationId}",
                title: $"Protect Refugees Near {eventData.LocationId}",
                description:
                    $"Refugees have gathered near {eventData.LocationId}. " +
                    $"They need protection from bandits and hostile factions.",
                questType: QuestType.Protection,
                importance: eventData.RefugeeCount >= 30 ? QuestImportance.Urgent : QuestImportance.High,
                locationId: eventData.LocationId,
                primaryFactionId: DemoIds.TownGuard,
                originReason: eventData.Description,
                expirationHours: 1f
            );
        }

        if (eventData.RefugeeCount >= 20)
        {
            TryGenerateQuest(
                questKey: $"Escort_Refugees_{eventData.LocationId}",
                title: $"Escort Refugees From {eventData.LocationId}",
                description:
                    $"A large refugee group near {eventData.LocationId} needs safe passage to a safer settlement.",
                questType: QuestType.Escort,
                importance: QuestImportance.High,
                locationId: eventData.LocationId,
                primaryFactionId: DemoIds.TownGuard,
                originReason: eventData.Description,
                expirationHours: 2f
            );
        }
    }

    private void OnSuppliesStolen(SuppliesStolenEvent eventData)
    {
        if (eventData.AmountStolen < 25)
            return;

        TryGenerateQuest(
            questKey: $"Recovery_{eventData.StolenResourceType}_{eventData.LocationId}",
            title: $"Recover Stolen {eventData.StolenResourceType}",
            description:
                $"{eventData.ResponsibleFactionId} stole vital {eventData.StolenResourceType} supplies near {eventData.LocationId}. " +
                $"Recovering them could prevent further suffering.",
            questType: QuestType.Recovery,
            importance: eventData.AmountStolen >= 75 ? QuestImportance.Urgent : QuestImportance.Medium,
            locationId: eventData.LocationId,
            primaryFactionId: DemoIds.MerchantsGuild,
            originReason: eventData.Description,
            expirationHours: 3f
        );
    }

    private void OnRoadDangerChanged(RoadDangerChangedEvent eventData)
    {
        if (eventData.NewDanger < 60)
            return;

        TryGenerateQuest(
            questKey: $"Escort_Caravan_{eventData.LocationId}",
            title: $"Escort Caravan Through {eventData.LocationId}",
            description:
                $"Road danger near {eventData.LocationId} has become severe. " +
                $"Caravans need armed escort to survive the route.",
            questType: QuestType.Escort,
            importance: eventData.NewDanger >= 80 ? QuestImportance.Urgent : QuestImportance.High,
            locationId: eventData.LocationId,
            primaryFactionId: DemoIds.MerchantsGuild,
            originReason: eventData.Description,
            expirationHours: 2f
        );
    }

    private void OnFactionConflictChanged(FactionConflictChangedEvent eventData)
    {
        if (eventData.NewConflictLevel < 50)
            return;

        TryGenerateQuest(
            questKey: $"Diplomacy_{eventData.FactionAId}_{eventData.FactionBId}",
            title: $"Ease Tensions Between {eventData.FactionAId} and {eventData.FactionBId}",
            description:
                $"Conflict between {eventData.FactionAId} and {eventData.FactionBId} is escalating. " +
                $"Someone may need to negotiate before open violence spreads.",
            questType: QuestType.Diplomacy,
            importance: eventData.NewConflictLevel >= 80 ? QuestImportance.Urgent : QuestImportance.High,
            locationId: "Regional",
            primaryFactionId: eventData.FactionBId,
            originReason: eventData.Description,
            expirationHours: 4f
        );
    }

    private void OnShortageChanged(ShortageChangedEvent eventData)
    {
        if (eventData.NewShortageLevel < 50)
            return;

        TryGenerateQuest(
            questKey: $"Relief_{eventData.ResourceType}_{eventData.LocationId}",
            title: $"Bring {eventData.ResourceType} Relief to {eventData.LocationId}",
            description:
                $"{eventData.LocationId} is suffering from a serious {eventData.ResourceType} shortage. " +
                $"Relief supplies are urgently needed.",
            questType: QuestType.Relief,
            importance: eventData.NewShortageLevel >= 80 ? QuestImportance.Urgent : QuestImportance.High,
            locationId: eventData.LocationId,
            primaryFactionId: DemoIds.MerchantsGuild,
            originReason: eventData.Description,
            expirationHours: 2f
        );
    }

    private void TryGenerateQuest(
        string questKey,
        string title,
        string description,
        QuestType questType,
        QuestImportance importance,
        string locationId,
        string primaryFactionId,
        string originReason,
        float expirationHours)
    {
        if (generatedQuestKeys.Contains(questKey))
        {
            Debug.Log($"[QUEST DEBUG] Quest already exists: {questKey}");
            return;
        }

        QuestState quest = new QuestState(
            questId: questKey,
            title: title,
            description: description,
            questType: questType,
            importance: importance,
            locationId: locationId,
            primaryFactionId: primaryFactionId,
            originReason: originReason,
            expirationHours: expirationHours
        );

        quests.Add(quest);
        generatedQuestKeys.Add(questKey);

        EventBus.Publish(new QuestGeneratedEvent(quest));

        Debug.Log(
            $"[QUEST GENERATED] {quest.Title} | " +
            $"Type: {quest.QuestType} | " +
            $"Importance: {quest.Importance} | " +
            $"Expires In: {quest.ExpirationHours} hours"
        );
    }

    public void CompleteFirstActiveQuest()
    {
        QuestState quest = GetFirstActiveQuest();

        if (quest == null)
        {
            Debug.Log("[QUEST DEBUG] No active quest to complete.");
            return;
        }

        quest.Status = QuestStatus.Completed;
        EventBus.Publish(new QuestCompletedEvent(quest));
    }

    public void FailFirstActiveQuest()
    {
        QuestState quest = GetFirstActiveQuest();

        if (quest == null)
        {
            Debug.Log("[QUEST DEBUG] No active quest to fail.");
            return;
        }

        quest.Status = QuestStatus.Failed;
        EventBus.Publish(new QuestFailedEvent(quest, "The player did not resolve the crisis."));
    }

    private QuestState GetFirstActiveQuest()
    {
        foreach (QuestState quest in quests)
        {
            if (quest.Status == QuestStatus.Active)
                return quest;
        }

        return null;
    }

    private void CheckForExpiredQuests()
    {
        foreach (QuestState quest in quests)
        {
            if (!quest.IsExpired())
                continue;

            quest.Status = QuestStatus.Expired;
            EventBus.Publish(new QuestExpiredEvent(quest));
        }
    }

    public void PrintActiveQuests()
    {
        bool foundAny = false;

        Debug.Log("[QUEST DEBUG] Active quests:");

        foreach (QuestState quest in quests)
        {
            if (quest.Status != QuestStatus.Active)
                continue;

            foundAny = true;

            Debug.Log(
                $"{quest.QuestId} | {quest.Title} | " +
                $"Type: {quest.QuestType} | " +
                $"Importance: {quest.Importance} | " +
                $"Location: {quest.LocationId}"
            );
        }

        if (!foundAny)
            Debug.Log("[QUEST DEBUG] No active quests.");
    }

    public void PrintAllQuests()
    {
        if (quests.Count == 0)
        {
            Debug.Log("[QUEST DEBUG] No quests generated.");
            return;
        }

        Debug.Log("[QUEST DEBUG] All quests:");

        foreach (QuestState quest in quests)
        {
            Debug.Log(
                $"{quest.QuestId} | {quest.Title} | " +
                $"Status: {quest.Status} | " +
                $"Type: {quest.QuestType} | " +
                $"Importance: {quest.Importance} | " +
                $"Origin: {quest.OriginReason}"
            );
        }
    }

    public void ClearQuests()
    {
        quests.Clear();
        generatedQuestKeys.Clear();

        Debug.Log("[QUEST DEBUG] All quests cleared.");
    }
}