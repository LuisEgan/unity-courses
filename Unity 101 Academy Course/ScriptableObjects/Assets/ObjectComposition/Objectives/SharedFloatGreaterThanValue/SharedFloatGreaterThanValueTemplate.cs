using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Missions/Objectives/SharedFloatGreaterThanValue")]
public class SharedFloatGreaterThanValueTemplate : MissionObjectiveTemplate
{
    [SerializeField] private SharedFloat sharedFloat;
    [SerializeField] private float maxValue;
    
    public override IMissionObjective GetObjective()
    {
        return new SharedFloatGreaterThanValue(sharedFloat, maxValue);
    }
}
