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

        GUILayout.BeginArea(new Rect(10, 10, 650, 450), GUI.skin.box);

        GUILayout.Label("<b>Fantasy Simulation Debug Overlay</b>");

        GUILayout.Space(8);

        GUILayout.Label("Recent Events:");

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(280));

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

        GUILayout.EndScrollView();

        GUILayout.Space(8);

        GUILayout.Label("Controls:");
        GUILayout.Label("F1 = Toggle overlay");
        GUILayout.Label("F2 = Print full timeline");
        GUILayout.Label("F3 = Replay recent events");
        GUILayout.Label("F4 = Replay all events");

        GUILayout.EndArea();
    }
}