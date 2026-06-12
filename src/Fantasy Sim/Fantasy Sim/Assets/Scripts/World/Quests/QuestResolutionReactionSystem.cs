using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestResolutionReactionSystem : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.Subscribe<QuestCompletedEvent>(OnQuestCompleted);
        EventBus.Subscribe<QuestFailedEvent>(OnQuestFailed);
        EventBus.Subscribe<QuestExpiredEvent>(OnQuestExpired);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<QuestCompletedEvent>(OnQuestCompleted);
        EventBus.Unsubscribe<QuestFailedEvent>(OnQuestFailed);
        EventBus.Unsubscribe<QuestExpiredEvent>(OnQuestExpired);
    }

    private void OnQuestCompleted(QuestCompletedEvent eventData)
    {
        QuestState quest = eventData.Quest;

        Debug.Log($"[QUEST RESOLUTION] Completed: {quest.Title}");

        switch (quest.QuestType)
        {
            case QuestType.Protection:
                Debug.Log("[WORLD REACTION] Refugees are safer. Local trust improves.");
                PublishFactionReward(quest, 15, "Player protected vulnerable refugees");
                break;

            case QuestType.Recovery:
                Debug.Log("[WORLD REACTION] Supplies are recovered. Shortage pressure eases.");
                PublishFactionReward(quest, 10, "Player recovered stolen supplies");
                break;

            case QuestType.Escort:
                Debug.Log("[WORLD REACTION] Travel becomes safer. Merchants spread good rumors.");
                PublishFactionReward(quest, 10, "Player escorted travelers safely");
                break;

            case QuestType.Diplomacy:
                Debug.Log("[WORLD REACTION] Political tensions ease.");
                PublishFactionReward(quest, 20, "Player helped reduce political conflict");
                break;

            case QuestType.Relief:
                Debug.Log("[WORLD REACTION] Relief reaches those in need.");
                PublishFactionReward(quest, 15, "Player delivered relief supplies");
                break;
        }
    }

    private void OnQuestFailed(QuestFailedEvent eventData)
    {
        QuestState quest = eventData.Quest;

        Debug.Log($"[QUEST RESOLUTION] Failed: {quest.Title}");

        switch (quest.QuestType)
        {
            case QuestType.Protection:
                Debug.Log("[WORLD REACTION] Refugees suffer. Fear spreads.");
                PublishFactionPenalty(quest, -10, "Player failed to protect refugees");
                break;

            case QuestType.Recovery:
                Debug.Log("[WORLD REACTION] Stolen supplies remain missing. Shortage worsens.");
                PublishFactionPenalty(quest, -10, "Player failed to recover supplies");
                break;

            case QuestType.Escort:
                Debug.Log("[WORLD REACTION] Travelers avoid the roads. Trade suffers.");
                PublishFactionPenalty(quest, -8, "Player failed an escort duty");
                break;

            case QuestType.Diplomacy:
                Debug.Log("[WORLD REACTION] Political tensions continue to rise.");
                PublishFactionPenalty(quest, -15, "Player failed diplomatic intervention");
                break;

            case QuestType.Relief:
                Debug.Log("[WORLD REACTION] People go without aid.");
                PublishFactionPenalty(quest, -12, "Player failed to deliver relief");
                break;
        }
    }

    private void OnQuestExpired(QuestExpiredEvent eventData)
    {
        QuestState quest = eventData.Quest;

        Debug.Log($"[QUEST RESOLUTION] Expired: {quest.Title}");
        Debug.Log("[WORLD REACTION] The world moved on without the player.");

        PublishFactionPenalty(quest, -5, $"Quest expired: {quest.Title}");
    }

    private void PublishFactionReward(QuestState quest, int reputationAmount, string reason)
    {
        EventBus.Publish(new FactionReputationChangeRequestedEvent(
            quest.PrimaryFactionId,
            reputationAmount,
            reason
        ));
    }

    private void PublishFactionPenalty(QuestState quest, int reputationAmount, string reason)
    {
        EventBus.Publish(new FactionReputationChangeRequestedEvent(
            quest.PrimaryFactionId,
            reputationAmount,
            reason
        ));
    }
}
