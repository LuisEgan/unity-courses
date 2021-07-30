using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Missions/Mission")]
public class MissionTemplate : ScriptableObject
{
    [SerializeField] private MissionObjectiveTemplate[] objectives;

    public Mission GetMission()
    {
        // Pass all objectives to the mission
        return new Mission(objectives.Select(objective => objective.GetObjective()).ToArray());
    }
}