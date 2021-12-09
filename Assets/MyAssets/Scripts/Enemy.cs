using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IArrowHittable
{
    public void Hit(Arrow arrow)
    {
        Debug.Log(name + " was hit");
    }

    /*private void ApplyMaterial()
    {
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        Material[] materials = skinnedMeshRenderer.materials;
        foreach (Material material in materials)
        {
            material.set
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
