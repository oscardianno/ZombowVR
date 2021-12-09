using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IArrowHittable
{
    [SerializeField]
    private int health = 100;
    private int damagePower = 5;

    public AudioClip deathAudio;
    public AudioClip doorHitAudio;
    public AudioClip doorHitAudio2;
    private Transform doorTransform;
    private NavMeshAgent agent;
    private DoorTarget doorTarget;

    private float attackTimer = 0;
    private float attackCooldown = 1.5f;
    private bool readyToAttack = false;
    
    private Animator anim;
    private int speedHash = Animator.StringToHash("Speed");
    private int aliveHash = Animator.StringToHash("Alive");
    private int attackHash = Animator.StringToHash("Attack");

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
        GetComponent<AudioSource>().PlayOneShot(deathAudio);
        anim.SetBool(aliveHash, false);
        //GetComponent<Animator>().enabled = false;
        Destroy(gameObject, 15);
        Destroy(this);
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
        anim = GetComponent<Animator>();
        anim.SetBool(aliveHash, true);
        agent = GetComponent<NavMeshAgent>();
        doorTarget = GameObject.FindGameObjectWithTag("Target").GetComponent<DoorTarget>();
        doorTransform = GameObject.FindGameObjectWithTag("Target").transform;

        if (doorTarget == null)
        {
            Debug.Log("Zombie with no doorTarget");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.isActiveAndEnabled)
        {
            // Stop the enemy movement when he has arrived the target
            if (Vector3.Distance(doorTransform.position, transform.position) < 2.0f)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ |
                    RigidbodyConstraints.FreezePositionX| RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                agent.enabled = false;
                agent.velocity = Vector3.zero;
                anim.SetFloat(speedHash, 0f);

                readyToAttack = true;
            } else
            {
                agent.SetDestination(doorTransform.position);
                anim.SetFloat(speedHash, agent.velocity.magnitude);
            }
        }

        if (readyToAttack)
        {
            if (attackTimer > attackCooldown)
            {
                Debug.Log(name + " is attacking");
                anim.SetTrigger(attackHash);
                attackTimer = 0;

                doorTarget.Hit(damagePower);
            }
            attackTimer += Time.deltaTime;
        }
    }
}
