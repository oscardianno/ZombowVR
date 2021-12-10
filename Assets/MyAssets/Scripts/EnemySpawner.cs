using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnTime = 1;
    public GameObject spawnGameObject;
    public Transform[] spawnPoints;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > spawnTime)
        {
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(spawnGameObject, randomPoint.position, randomPoint.rotation);
            nextRandomSpawnTime();
        }
        timer += Time.deltaTime;
    }

    private void nextRandomSpawnTime()
    {
        timer = 0;
        spawnTime = Random.Range(2, 5);
    }
}
