using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationDebugOverlay : MonoBehaviour
{
    [SerializeField] private SimulationTimeline simulationTimeline;
    [SerializeField] private DynamicQuestSystems dynamicQuestSystem;
    [SerializeField] private UtilityAISystem utilityAISystem;

    [SerializeField] private bool showOverlay = true;
    [SerializeField] private int recentEventCount = 8;

    private Vector2 scrollPosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            showOverlay = !showOverlay;
        }
    }

    private void OnGUI()
    {
        if (!showOverlay)
            return;

        GUILayout.BeginArea(new Rect(10, 10, 720, 650), GUI.skin.box);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        GUILayout.Label("<b>Fantasy Simulation Debug Overlay</b>");
        GUILayout.Label("Press F1 to hide/show this panel.");

        GUILayout.Space(8);

        GUILayout.Label("<b>Recent Events</b>");

        List<TimelineEntry> recentEntries = simulationTimeline.GetRecentEntries(recentEventCount);

        if (recentEntries.Count == 0)
        {
            GUILayout.Label("No events recorded yet.");
        }
        else
        {
            foreach (TimelineEntry entry in recentEntries)
            {
                GUILayout.Label(entry.ToDisplayString());
            }
        }

        GUILayout.Space(12);

        GUILayout.Label("<b>Controls</b>");

        GUILayout.Label("Overlay / Debug:");
        GUILayout.Label("F1 = Toggle overlay");
        GUILayout.Label("F2 = Print full timeline");
        GUILayout.Label("F3 = Replay recent events");
        GUILayout.Label("F4 = Replay all events");
        GUILayout.Label("F5 = Print world-state inspector");

        GUILayout.Space(5);

        GUILayout.Label("Core Event Demos:");
        GUILayout.Label("1 = Steal item");
        GUILayout.Label("2 = Attack Town Guard member");
        GUILayout.Label("3 = Manual witnessed crime");
        GUILayout.Label("4 = Attack Thieves' Circle member");
        GUILayout.Label("5 = Steal bread from baker");
        GUILayout.Label("6 = Help baker");
        GUILayout.Label("7 = Betray companion");
        GUILayout.Label("8 = Attack baker");

        GUILayout.Space(5);

        GUILayout.Label("NPC Interaction Demos:");
        GUILayout.Label("B = Talk to baker");
        GUILayout.Label("G = Talk to guard");
        GUILayout.Label("C = Talk to companion");
        GUILayout.Label("T = Talk to villager");
        GUILayout.Label("V = Save villager");

        GUILayout.Space(5);

        GUILayout.Label("Dynamic Quest / World Pressure:");
        GUILayout.Label("K = Bandit attack caravan");
        GUILayout.Label("L = Major bandit raid");
        GUILayout.Label("Q = Print active quests");
        GUILayout.Label("P = Print all quests");
        GUILayout.Label("= = Complete first active quest");
        GUILayout.Label("- = Fail first active quest");
        GUILayout.Label("Backspace = Clear quests");

        GUILayout.Space(5);

        GUILayout.Label("Utility AI:");
        GUILayout.Label("U = Evaluate NPC goals");
        GUILayout.Label("Y = Print current NPC goals");

        GUILayout.Space(5);

        GUILayout.Label("System Debug:");
        GUILayout.Label("F9 = Print shortages");
        GUILayout.Label("F10 = Print refugees");
        GUILayout.Label("F11 = Print road danger");
        GUILayout.Label("F12 = Print faction conflicts");

        GUILayout.Space(5);

        GUILayout.Label("Full Demo:");
        GUILayout.Label("O = Run observability stress test");

        GUILayout.EndScrollView();

        GUILayout.EndArea();
    }
}