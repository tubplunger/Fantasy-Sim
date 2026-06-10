using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicQuestSystems : MonoBehaviour
{
    private readonly List<QuestState> activeQuests = new();

    private readonly HashSet<string> generatedQuestKeys = new();

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
                originReason: eventData.Description
            );
        }

        if (eventData.RefugeeCount >= 20)
        {
            TryGenerateQuest(
                questKey: $"Escort_Refugees_{eventData.LocationId}",
                title: $"Escort Refugees From {eventData.LocationId}",
                description:
                    $"A large refugee group near {eventData.LocationId} needs safe passage to a more stable settlement.",
                questType: QuestType.Escort,
                originReason: eventData.Description
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
            originReason: eventData.Description
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
            originReason: eventData.Description
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
            originReason: eventData.Description
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
            originReason: eventData.Description
        );
    }

    private void TryGenerateQuest(
        string questKey,
        string title,
        string description,
        QuestType questType,
        string originReason)
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
            originReason: originReason
        );

        activeQuests.Add(quest);
        generatedQuestKeys.Add(questKey);

        EventBus.Publish(new QuestGeneratedEvent(quest));

        Debug.Log(
            $"[QUEST GENERATED] {quest.Title} | " +
            $"Type: {quest.QuestType} | " +
            $"Origin: {quest.OriginReason}"
        );
    }

    public void PrintActiveQuests()
    {
        if (activeQuests.Count == 0)
        {
            Debug.Log("[QUEST DEBUG] No active quests.");
            return;
        }

        Debug.Log("[QUEST DEBUG] Active quests:");

        foreach (QuestState quest in activeQuests)
        {
            Debug.Log(
                $"{quest.QuestId} | {quest.Title} | " +
                $"Type: {quest.QuestType} | Origin: {quest.OriginReason}"
            );
        }
    }

    public void ClearQuests()
    {
        activeQuests.Clear();
        generatedQuestKeys.Clear();

        Debug.Log("[QUEST DEBUG] All quests cleared.");
    }
}
