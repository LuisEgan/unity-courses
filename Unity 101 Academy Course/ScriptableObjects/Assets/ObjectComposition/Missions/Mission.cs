using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mission
{
    private IMissionObjective[] _objectives;
    private IMissionReward[] _rewards;

    public Mission(IMissionObjective[] objectives)
    {
        _objectives = objectives;
    }

    public bool IsMissionAccomplished()
    {
        return _objectives.All(objective => objective.IsCompleted());
    }
}