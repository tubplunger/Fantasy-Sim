using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStateInspector : MonoBehaviour
{
    [Header("Timeline")]
    [SerializeField] private SimulationTimeline simulationTimeline;
    [SerializeField] private SimulationReplaySystem replaySystem;

    [Header("World State")]
    [SerializeField] private ShortageSystem shortageSystem;
    [SerializeField] private MigrationSystem migrationSystem;
    [SerializeField] private RoadDangerSystem roadDangerSystem;
    [SerializeField] private FactionConflictSystem factionConflictSystem;
    [SerializeField] private DynamicQuestSystems dynamicQuestSystem;
    [SerializeField] private UtilityAISystem utilityAISystem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            simulationTimeline.PrintTimeline();
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            replaySystem.ReplayRecentEvents(15);
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            replaySystem.ReplayAllEvents();
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            PrintWorldState();
        }
    }

    public void PrintWorldState()
    {
        Debug.Log("========== WORLD STATE INSPECTOR ==========");

        shortageSystem.PrintShortages();
        migrationSystem.PrintRefugees();
        roadDangerSystem.PrintRoadDanger();
        factionConflictSystem.PrintConflicts();
        dynamicQuestSystem.PrintAllQuests();
        utilityAISystem.PrintCurrentGoals();

        Debug.Log("===========================================");
    }
}
