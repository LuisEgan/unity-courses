using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private MissionTemplate[] missionTemplates;

    private List<Mission>  _missions;
    private List<Mission> _toRemove;

    private void Awake()
    {
        _missions = missionTemplates.Select(t => t.GetMission()).ToList();
    }

    private void Update()
    {
        if (_missions.All(m => m.IsMissionAccomplished()))
        {
            print("You did it!");
        }
    }
}