using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Binary : MonoBehaviour
{
    public static int[] GetUnlockedLevels(int levelsBinary)
    {
        List<int> unlockedLevels = new List<int>();
        
        for (int i = 0; i < 32; i++)
        {
            if ((levelsBinary & (1 << i)) != 0)
            {
                unlockedLevels.Add(i);
            }
        }

        return unlockedLevels.ToArray();
    }
}