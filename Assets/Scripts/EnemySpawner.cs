using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public float spawnTime = 3.0f;
    public int spawnCount = 1;
    float currentSpawnTime;
    int currentSpawnCount;

    // Start is called before the first frame update
    void Start()
    {
        currentSpawnTime = spawnTime;
        currentSpawnCount = spawnCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpawnCount == 0)
        {
            return;
        }

        currentSpawnTime -= Time.deltaTime;
        if (currentSpawnTime < 0)
        {
            GameObject.Instantiate(enemyToSpawn, transform.position + Vector3.up * 0.501f, transform.GetChild(0).rotation);
            currentSpawnTime = spawnTime;
            currentSpawnCount--;
        }
    }
}
