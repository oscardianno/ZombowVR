using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IArrowHittable
{
    [SerializeField]
    private int health = 100;

    private Transform target;
    private NavMeshAgent agent;

    public void Hit(Arrow arrow)
    {
        Debug.Log(name + " was hit");
        health -= 100;

        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        // Play Death animation
        // Play sound
        agent.enabled = false;
        GetComponent<Animator>().enabled = false;
        Destroy(this, 15);
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
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Target").transform;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }
}
