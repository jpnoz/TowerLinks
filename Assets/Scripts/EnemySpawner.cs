using UnityEngine;

[SelectionBase]
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public float spawnTime = 3.0f;
    public int spawnCount = 1;
    float currentSpawnTime;
    int currentSpawnCount;
    bool isWaveActive = false;

    public delegate void FinishedSpawning();
    public static event FinishedSpawning OnFinishedSpawning;

    private void OnEnable()
    {
        WaveSpawner.OnNewWave += StartWave;
    }

    private void OnDisable()
    {
        WaveSpawner.OnNewWave -= StartWave;
    }


    void Start()
    {
        currentSpawnTime = spawnTime;
        currentSpawnCount = spawnCount;
    }

    void StartWave(int spawnCount, float spawnTime, int spawnHealthBoost)
    {
        this.spawnCount = spawnCount;
        this.spawnTime = spawnTime;

        currentSpawnCount = spawnCount;

        isWaveActive = true;
    }

    void Update()
    {
        if (!isWaveActive)
        {
            return;
        }

        if (currentSpawnCount <= 0)
        {
            isWaveActive = false;
            OnFinishedSpawning?.Invoke();
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

        
    }
}
