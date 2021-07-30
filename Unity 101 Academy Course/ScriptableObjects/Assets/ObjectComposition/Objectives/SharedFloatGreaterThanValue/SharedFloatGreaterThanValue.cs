using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedFloatGreaterThanValue : IMissionObjective
{
    private readonly SharedFloat _sharedFloat;
    private readonly float _maxValue;

    public SharedFloatGreaterThanValue(SharedFloat sharedFloat, float maxValue)
    {
        _sharedFloat = sharedFloat;
        _maxValue = maxValue;
    }
    
    public bool IsCompleted()
    {
        return _sharedFloat.Value > _maxValue;
    }
} 
