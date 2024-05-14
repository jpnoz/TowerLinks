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


    void Start()
    {
        currentSpawnTime = spawnTime;
        currentSpawnCount = spawnCount;
    }


    void Update()
    {
        if (currentSpawnCount == 0)
        {
            return;
        }

        currentSpawnTime -= Time.deltaTime;
        if (currentSpawnTime < 0)
        {
            SpawnEnemy();
            currentSpawnTime = spawnTime;
            currentSpawnCount--;
        }
    }

    void SpawnEnemy()
    {

        GameObject newEnemy = Instantiate(enemyToSpawn, transform.position + Vector3.up * 0.501f, transform.GetChild(0).rotation);

        CapsuleCollider capsuleCollider = newEnemy.GetComponent<CapsuleCollider>();
        if (capsuleCollider == null)
        {
            capsuleCollider = newEnemy.AddComponent<CapsuleCollider>();
            capsuleCollider.isTrigger = true; 
        }
        else
        {
            capsuleCollider.isTrigger = true;
        }


        CapsuleHealth capsuleHealth = newEnemy.GetComponent<CapsuleHealth>();
        if (capsuleHealth == null)
        {
            
            capsuleHealth = newEnemy.AddComponent<CapsuleHealth>();
        }
    }
}
