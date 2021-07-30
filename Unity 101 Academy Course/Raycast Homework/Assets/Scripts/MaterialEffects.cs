using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialEffects : MonoBehaviour
{
    // HOMEWORK
    // Create 2 new material effects by changing it's parameteres
    // GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 1);

    private Material material;

    public enum Effects
    {
        palpitation,
        harden
    }

    public Effects effect;

    private void Awake()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        
        if (mr == null)
        {
            material = GetComponent<SkinnedMeshRenderer>().material;
        }
        else
        {
            material = mr.material;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (effect == Effects.palpitation)
        {
            Palpitation();
        }
        else if (effect == Effects.harden)
        {
            Harden();
        }
    }

    void Palpitation()
    {
        float palpitaitonValue = Mathf.Abs((float) Math.Sin(Time.time * 4));
        material.SetFloat("_Parallax", palpitaitonValue);
    }

    void Harden()
    {
        material.SetFloat("_Metallic", 1);
        material.SetFloat("_SmoothnessTextureChannel", .5f);
    }
}