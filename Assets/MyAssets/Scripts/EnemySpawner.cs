using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnGameObject;
    [SerializeField]
    private Transform[] spawnPoints;
    private int spawnedCount = 0;
    private float minSpawnTime = 4f;
    private float maxSpawnTime = 8f;
    private float spawnTime = 2f;
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
            NextRandomSpawnTime();

            spawnedCount++;
            // Decrease the min and max spawnTimes each 5 enemies spawned
            if (spawnedCount % 5 == 0)
            {
                // With a lowest minimum of 1 second from spawn
                if (minSpawnTime > 1f)
                {
                    minSpawnTime -= 0.5f;
                }
                if (maxSpawnTime > 1f)
                {
                    maxSpawnTime -= 0.5f;
                }
            }
        }
        timer += Time.deltaTime;
    }

    private void NextRandomSpawnTime()
    {
        timer = 0;
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
