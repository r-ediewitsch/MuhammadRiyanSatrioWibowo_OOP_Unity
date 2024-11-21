using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    private int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 3;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;

    public bool isSpawning = false;
    private bool spawningRunning = false;

    private void Start()
    {
        spawnCount = defaultSpawnCount;
    }

    private void Update()
    {   
        if (isSpawning && !spawningRunning)
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        spawningRunning = true;
        
        while (isSpawning && spawnCount > 0)
        {  
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
            spawnCount--;
        }

        spawningRunning = false;
        isSpawning = false;
    }

    private void SpawnEnemy()
    {
        Enemy enemyInstance = Instantiate(spawnedEnemy);
        enemyInstance.combatManager = combatManager;
        enemyInstance.spawner = this;
    }

    public void RegisterEnemyDeath()
    {
        totalKill++;
        totalKillWave++;

        if(totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            totalKillWave = 0;
            defaultSpawnCount += spawnCountMultiplier*multiplierIncreaseCount;
            multiplierIncreaseCount++;
        }
    }
}
