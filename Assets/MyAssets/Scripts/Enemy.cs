using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IArrowHittable
{
    [SerializeField]
    private int health = 100;
    private int damagePower = 5;

    private Transform doorTransform;
    private NavMeshAgent agent;
    private DoorTarget doorTarget;

    private float attackTimer = 0;
    private float attackCooldown = 1.5f;
    private bool readyToAttack = false;
    
    private Animator anim;
    private int speedHash = Animator.StringToHash("Speed");
    private int aliveHash = Animator.StringToHash("Alive");
    private int dieHash = Animator.StringToHash("Die");
    private int attackHash = Animator.StringToHash("Attack");

    private int hitClipsNo;
    private int moanClipsNo;
    private int spawnClipsNo;
    private int impactClipsNo;
    public AudioClip[] hitClips;
    public AudioClip[] moanClips;
    public AudioClip[] spawnClips;
    public AudioClip[] impactClips;
    private AudioSource audioSource;


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
        //agent = null; 
        doorTarget = GameObject.FindGameObjectWithTag("Target").GetComponent<DoorTarget>();
        doorTransform = GameObject.FindGameObjectWithTag("Target").transform;

        audioSource = GetComponent<AudioSource>();
        hitClipsNo = hitClips.Length;
        spawnClipsNo = spawnClips.Length;
        impactClipsNo = impactClips.Length;

        // 50/50 chance to play a sound
        if (Random.Range(0,2) == 1){
            PlayRandomAudioClip(spawnClips, spawnClipsNo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (doorTarget != null)
        {*/
            if (agent.isActiveAndEnabled)
            {
                // Stop the enemy movement when he has arrived the target
                if (Vector3.Distance(doorTransform.position, transform.position) < 2.0f)
                {
                    // Avoid the enemy's Rigidbody to bounce or do weird stuff
                    //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ |
                    //    RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                    agent.enabled = false;
                    agent.velocity = Vector3.zero;
                    // Pass 0 speed to animator
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
                    //Debug.Log(name + " is attacking");
                    anim.SetTrigger(attackHash);
                    attackTimer = 0;

                    doorTarget.Hit(damagePower);
                    // 1/5 chance to play a moan
                    if (Random.Range(0, 6) == 1)
                    {
                        PlayRandomAudioClip(moanClips, moanClipsNo);
                    }
                    PlayRandomAudioClip(impactClips, impactClipsNo);
                }
                attackTimer += Time.deltaTime;
            }
        /*} 
        else
        {
            // If there's no doorTransform anymore, it was destroyed and it's a game over
            //agent.SetDestination(GetComponent("GameOverTarget").transform.position);
            agent.SetDestination(GameObject.FindGameObjectWithTag("GameOverTarget").GetComponent<GameObject>().transform.position);
            anim.SetFloat(speedHash, agent.velocity.magnitude);
        }*/
    }

    private void PlayRandomAudioClip(AudioClip[] audioClips, int audioClipsNo)
    {
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClipsNo)]);
    }
    public void Hit(Arrow arrow)
    {
        //Debug.Log(name + " was hit");
        PlayRandomAudioClip(hitClips, hitClipsNo);

        health -= 100;
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        //Debug.Log(name + " has died");
        agent.enabled = false;
        // Play sound
        PlayRandomAudioClip(hitClips, hitClipsNo);
        // Play Death animation
        anim.SetTrigger(dieHash);

        Destroy(this);
        Destroy(gameObject, 2);
    }
}
