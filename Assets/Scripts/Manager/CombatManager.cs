using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;

    [SerializeField] private bool isWaveActive = false;

    private void Start()
    {
        // if(timer >= waveInterval)
        // StartWave();
    }

    private void Update()
    {
        if (isWaveActive && totalEnemies <= 0)
        {
            EndWave();
        }

        if (!isWaveActive)
        {
            timer += Time.deltaTime;
            if (timer >= waveInterval)
            {
                StartWave();
                timer = 0;
            }
        }
    }

    private void StartWave()
    {
        waveNumber++;
        isWaveActive = true;
        totalEnemies = 0;

        foreach (var spawner in enemySpawners)
        {
            // spawner.spawnCount = spawner.defaultSpawnCount;
            spawner.isSpawning = true;
            totalEnemies += 1;
            StartCoroutine(TrackEnemiesFromSpawner(spawner));
        }
    }

    private IEnumerator TrackEnemiesFromSpawner(EnemySpawner spawner)
    {
        while (spawner.isSpawning)
        {
            yield return null;
        }
    }

    private void EndWave()
    {
        isWaveActive = false;
    }

    public void RegisterEnemyDeath()
    {
        totalEnemies--;
    }

    public int GetTotalKills()
    {
        return waveNumber * 10;
    }
}
