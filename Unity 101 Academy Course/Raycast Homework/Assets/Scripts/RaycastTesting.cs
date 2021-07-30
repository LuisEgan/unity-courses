using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTesting : MonoBehaviour
{
    private Transform target;

    [SerializeField] private LayerMask mask;
    
    // Bits operations
    // Unlocked levels example
    // int unlockedLevels = 1000 = 1<<3
    // if(unlockedLevels & (1<<3) > 0) -> level 3 is unlocked!
    // this is because this represent a binary operation which compares bit by bit
    // if unlockedLeves is, for example = 100010
    // and the level 3 is 1000
    // the operation is as follows:
    // unlockedLeves - 100010
    // level 3       - 001000
    // result        - 000000, because it's comparing by columns if both are 1, like false & false = false
    // so 0 & 0 = 0
    // 1 & 0 = 0
    // 1 & 1 = 1
    
    // the operation is also
    // unlockedLevels - true  false false false true  false
    // level 3        - false false true  false false false
    // result of &    - false false false false false false ---> false


    // Start is called before the first frame update
    void Start()
    {
        // HOMEWORK
        // on start print which levels are unlockedd
        int levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked");
        int[] levels = Binary.GetUnlockedLevels(levelsUnlocked);

        foreach (int level in levels)
        {
            print(level);
        }
        
        RaycastHit hitInfo;
        // layerMask is given in binary, it's an int which bits represent the contained layers
        // so "1" registers only collision with the Default layer (which the 0 one in the unity inspector)

        // << means that you translate in bits
        // 00001 << 2 would be
        // 00100 (moved the 1 twice to the left)
        // it's a way of multiplication in bits!
        // to get the correct layer, would be 1 << [target layer] (in Unity's inspector)
        if (Physics.Raycast(
            transform.position,
            transform.forward,
            out hitInfo, 100,
            // to add the layer(s) from the inspector
            mask
            // to add the layer by it's name
            // 1 << LayerMask.NameToLayer("Water")
            // to add more layers to collide with:
            // 1<<5 | 1<<4 
        ))
        {
            target = hitInfo.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            // returns the rotation of the forward of the object pointing towards another
//            transform.rotation = Quaternion.LookRotation(target.position - transform.position);

            // Another way
            // transform.forward is always normalized atuomatically    
            transform.forward = target.position - transform.position;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("levelsUnlocked", (1 << 3) | (1 << 2));
    }
}