using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public int[] spawnCounts;
    public float[] spawnTimes;
    public int[] spawnHealthBoosts;
    public int currentWave = -1;

    public delegate void WaveStarted(int spawnCount, float spawnTime, int spawnHealthBoost);
    public static event WaveStarted OnNewWave;

    public void StartWave ()
    {
        currentWave++;
        OnNewWave?.Invoke(spawnCounts[currentWave], spawnTimes[currentWave], spawnHealthBoosts[currentWave]);
    }
}
