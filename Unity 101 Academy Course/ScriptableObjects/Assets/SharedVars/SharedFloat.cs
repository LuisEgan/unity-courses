using System;
using UnityEngine;

[CreateAssetMenu (fileName = "SharedFloat", menuName = "SharedVars/SharedFloat")]
public class SharedFloat : ScriptableObject
{
    public event Action<float> OnValueChanged;

    private float _value;
    
    public float Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }
    
    private void OnEnable()
    {
        Value = 0;
    }
}