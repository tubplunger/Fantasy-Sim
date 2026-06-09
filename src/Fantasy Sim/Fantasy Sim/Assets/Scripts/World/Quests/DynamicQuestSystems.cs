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
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<RefugeesArrivedEvent>(OnRefugeesArrived);
    }

    private void OnRefugeesArrived(RefugeesArrivedEvent eventData)
    {
        if (eventData.RefugeeCount < 10)
            return;

        string questKey = $"ProtectRefugees_{eventData.LocationId}";

        if (generatedQuestKeys.Contains(questKey))
        {
            Debug.Log($"[QUEST DEBUG] Protection quest for {eventData.LocationId} already exists.");
            return;
        }

        QuestState quest = new QuestState(
            questId: questKey,
            title: $"Protect Refugees Near {eventData.LocationId}",
            description:
                $"Refugees have gathered near {eventData.LocationId} after worsening shortages. " +
                $"They need protection from bandits and hostile factions.",
            questType: QuestType.Protection,
            originReason: eventData.Description
        );

        activeQuests.Add(quest);
        generatedQuestKeys.Add(questKey);

        EventBus.Publish(new QuestGeneratedEvent(quest));

        Debug.Log(
            $"[QUEST GENERATED]\n" +
            $"Title: {quest.Title}\n" +
            $"Type: {quest.QuestType}\n" +
            $"Description: {quest.Description}\n" +
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
}
