using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAISystem : MonoBehaviour
{
    [SerializeField] private RelationshipSystem relationshipSystem;
    [SerializeField] private NPCMemorySystem memorySystem;
    [SerializeField] private RoadDangerSystem roadDangerSystem;
    [SerializeField] private ShortageSystem shortageSystem;

    private readonly List<NPCUtilityProfile> npcProfiles = new();

    private const string PlayerId = "Player";
    private const string DemoLocationId = "Eastroad";

    private void Awake()
    {
        CreateDemoNPCs();
    }

    private void CreateDemoNPCs()
    {
        npcProfiles.Add(new NPCUtilityProfile(
            npcId: "npc_baker_001",
            displayName: "Baker Tomas",
            factionId: DemoIds.MerchantsGuild,
            fear: 35,
            greed: 60,
            loyalty: 25,
            revenge: 30,
            survival: 45
        ));

        npcProfiles.Add(new NPCUtilityProfile(
            npcId: "npc_guard_001",
            displayName: "Guard Ren",
            factionId: DemoIds.TownGuard,
            fear: 15,
            greed: 10,
            loyalty: 80,
            revenge: 45,
            survival: 50
        ));

        npcProfiles.Add(new NPCUtilityProfile(
            npcId: "npc_villager_001",
            displayName: "Lena the Villager",
            factionId: DemoIds.TownGuard,
            fear: 45,
            greed: 15,
            loyalty: 30,
            revenge: 10,
            survival: 70
        ));

        npcProfiles.Add(new NPCUtilityProfile(
            npcId: "npc_companion_001",
            displayName: "Companion Mira",
            factionId: DemoIds.TownGuard,
            fear: 20,
            greed: 5,
            loyalty: 75,
            revenge: 35,
            survival: 60
        ));
    }

    public void EvaluateAllNPCs()
    {
        Debug.Log("[UTILITY AI] Evaluating all NPC goals...");

        foreach (NPCUtilityProfile npc in npcProfiles)
        {
            NPCUtilityDecision decision = EvaluateNPC(npc);

            EventBus.Publish(new NPCDecisionMadeEvent(decision));

            Debug.Log(
                $"[UTILITY AI] {decision.NpcName} chooses {decision.ChosenGoal} " +
                $"Score: {decision.Score:0.0} | Reason: {decision.Reason}"
            );

            npc.CurrentGoal = decision.ChosenGoal;
            npc.CurrentGoalScore = decision.Score;

            ExecuteGoal(npc, decision);
        }
    }

    private NPCUtilityDecision EvaluateNPC(NPCUtilityProfile npc)
    {
        float stayCalmScore = ScoreStayCalm(npc);
        float fleeDangerScore = ScoreFleeDanger(npc);
        float seekRevengeScore = ScoreSeekRevenge(npc);
        float joinFactionScore = ScoreJoinFaction(npc);
        float protectAllyScore = ScoreProtectAlly(npc);
        float exploitShortageScore = ScoreExploitShortage(npc);

        Debug.Log(
            $"[UTILITY SCORES] {npc.DisplayName} | " +
            $"Calm: {stayCalmScore:0.0} | " +
            $"Flee: {fleeDangerScore:0.0} | " +
            $"Revenge: {seekRevengeScore:0.0} | " +
            $"Join: {joinFactionScore:0.0} | " +
            $"Protect: {protectAllyScore:0.0} | " +
            $"Exploit: {exploitShortageScore:0.0}"
        );

        NPCGoalType bestGoal = NPCGoalType.StayCalm;
        float bestScore = stayCalmScore;
        string bestReason = $"No urgent need. Calm score {stayCalmScore:0.0}.";

        ConsiderGoal(NPCGoalType.FleeDanger, fleeDangerScore, $"Fear/survival and road danger are high.", ref bestGoal, ref bestScore, ref bestReason);
        ConsiderGoal(NPCGoalType.SeekRevenge, seekRevengeScore, $"Negative memory and revenge drive are high.", ref bestGoal, ref bestScore, ref bestReason);
        ConsiderGoal(NPCGoalType.JoinFaction, joinFactionScore, $"Survival pressure is high and safety is uncertain.", ref bestGoal, ref bestScore, ref bestReason);
        ConsiderGoal(NPCGoalType.ProtectAlly, protectAllyScore, $"Loyalty and ally concern are high.", ref bestGoal, ref bestScore, ref bestReason);
        ConsiderGoal(NPCGoalType.ExploitShortage, exploitShortageScore, $"Greed and shortage pressure are high.", ref bestGoal, ref bestScore, ref bestReason);

        if (npc.CurrentGoal != NPCGoalType.StayCalm && bestGoal != npc.CurrentGoal)
        {
            if (bestScore < npc.CurrentGoalScore + 15)
            {
                bestGoal = npc.CurrentGoal;
                bestScore = npc.CurrentGoalScore;
                bestReason = "Continuing current goal due to goal commitment.";
            }
        }

        return new NPCUtilityDecision(
            npc.NpcId,
            npc.DisplayName,
            bestGoal,
            bestScore,
            bestReason
        );
    }

    private void ConsiderGoal(
        NPCGoalType goal,
        float score,
        string reason,
        ref NPCGoalType bestGoal,
        ref float bestScore,
        ref string bestReason)
    {
        if (score <= bestScore)
            return;

        bestGoal = goal;
        bestScore = score;
        bestReason = reason;
    }

    private float ScoreStayCalm(NPCUtilityProfile npc)
    {
        RelationshipState relationship = relationshipSystem.GetRelationship(npc.NpcId, PlayerId);

        float calmScore = 25;

        calmScore += relationship.Trust * 0.2f;
        calmScore -= relationship.Fear * 0.3f;
        calmScore -= relationship.Suspicion * 0.2f;

        return Mathf.Clamp(calmScore, 0, 100);
    }

    private float ScoreFleeDanger(NPCUtilityProfile npc)
    {
        RelationshipState relationship = relationshipSystem.GetRelationship(npc.NpcId, PlayerId);
        int roadDanger = roadDangerSystem.GetRoadDanger(DemoLocationId);

        float score = 0;

        score += npc.Fear * 0.35f;
        score += npc.Survival * 0.35f;
        score += relationship.Fear * 0.4f;
        score += relationship.Suspicion * 0.2f;
        score += roadDanger * 0.35f;

        return Mathf.Clamp(score, 0, 100);
    }

    private float ScoreSeekRevenge(NPCUtilityProfile npc)
    {
        RelationshipState relationship = relationshipSystem.GetRelationship(npc.NpcId, PlayerId);

        NPCMemory violenceMemory = memorySystem.GetStrongestMemoryOf(
            npc.NpcId,
            MemoryType.Violence,
            PlayerId
        );

        NPCMemory betrayalMemory = memorySystem.GetStrongestMemoryOf(
            npc.NpcId,
            MemoryType.Betrayal,
            PlayerId
        );

        float violenceStrength = violenceMemory != null ? Mathf.Abs(violenceMemory.GetCurrentStrength()) : 0;
        float betrayalStrength = betrayalMemory != null ? Mathf.Abs(betrayalMemory.GetCurrentStrength()) : 0;

        float score = 0;

        score += npc.Revenge * 0.45f;
        score += violenceStrength * 0.35f;
        score += betrayalStrength * 0.5f;
        score += Mathf.Abs(Mathf.Min(relationship.Trust, 0)) * 0.25f;
        score -= relationship.Fear * 0.3f;

        return Mathf.Clamp(score, 0, 100);
    }

    private float ScoreJoinFaction(NPCUtilityProfile npc)
    {
        int roadDanger = roadDangerSystem.GetRoadDanger(DemoLocationId);
        int foodShortage = shortageSystem.GetShortageLevel(DemoLocationId, "Food");

        float score = 0;

        score += npc.Survival * 0.35f;
        score += roadDanger * 0.25f;
        score += foodShortage * 0.25f;
        score -= npc.Loyalty * 0.15f;

        return Mathf.Clamp(score, 0, 100);
    }

    private float ScoreProtectAlly(NPCUtilityProfile npc)
    {
        RelationshipState companionRelationship = relationshipSystem.GetRelationship(npc.NpcId, PlayerId);
        int roadDanger = roadDangerSystem.GetRoadDanger(DemoLocationId);

        float score = 0;

        score += npc.Loyalty * 0.45f;
        score += companionRelationship.Trust * 0.25f;
        score += roadDanger * 0.2f;
        score -= npc.Fear * 0.2f;

        return Mathf.Clamp(score, 0, 100);
    }

    private float ScoreExploitShortage(NPCUtilityProfile npc)
    {
        int foodShortage = shortageSystem.GetShortageLevel(DemoLocationId, "Food");

        float score = 0;

        score += npc.Greed * 0.5f;
        score += foodShortage * 0.4f;
        score -= npc.Loyalty * 0.2f;

        return Mathf.Clamp(score, 0, 100);
    }

    private void ExecuteGoal(NPCUtilityProfile npc, NPCUtilityDecision decision)
    {
        switch (decision.ChosenGoal)
        {
            case NPCGoalType.FleeDanger:
                Debug.Log($"[NPC ACTION] {npc.DisplayName} flees from danger near Eastroad.");
                break;

            case NPCGoalType.SeekRevenge:
                Debug.Log($"[NPC ACTION] {npc.DisplayName} starts planning revenge against the player.");
                break;

            case NPCGoalType.JoinFaction:
                Debug.Log($"[NPC ACTION] {npc.DisplayName} seeks protection by joining {DemoIds.TownGuard}.");
                break;

            case NPCGoalType.ProtectAlly:
                Debug.Log($"[NPC ACTION] {npc.DisplayName} moves to protect an ally.");
                break;

            case NPCGoalType.ExploitShortage:
                Debug.Log($"[NPC ACTION] {npc.DisplayName} raises prices due to the shortage.");
                break;

            case NPCGoalType.StayCalm:
                Debug.Log($"[NPC ACTION] {npc.DisplayName} continues normal routine.");
                break;
        }
    }

    public void PrintCurrentGoals()
    {
        Debug.Log("[Utility AI Debug] Current NPC goals: ");

        foreach (NPCUtilityProfile npc in npcProfiles)
        {
            Debug.Log(
           $"{npc.DisplayName} | Current Goal: {npc.CurrentGoal} | " +
           $"Score: {npc.CurrentGoalScore:0.0}"
       );
        }
    }
}
