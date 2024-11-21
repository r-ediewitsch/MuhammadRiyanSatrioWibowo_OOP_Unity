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

    private void Start()
    {
        spawnCount = defaultSpawnCount;
        if (isSpawning)
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    private void Update()
    {
        // totalKill = combatManager.GetTotalKills(); 

        // if (totalKill >= minimumKillsToIncreaseSpawnCount)
        // {
        //     IncreaseSpawnCount();
        //     totalKillWave = 0;
        // }
        
        // if (totalKillWave >= multiplierIncreaseCount)
        // {
        //     IncreaseMultiplier();
        //     totalKillWave = 0;
        // }
    }

    private IEnumerator SpawnEnemies()
    {
        while (isSpawning && spawnCount > 0)
        {  
            yield return new WaitForSeconds(spawnInterval);

            if(spawnedEnemy.level == combatManager.waveNumber - 1)
            {
                SpawnEnemy();
                spawnCount--;
            }
        }
        isSpawning = false;
    }

    private void SpawnEnemy()
    {
        Instantiate(spawnedEnemy);
        combatManager.totalEnemies++;
    }

    private void IncreaseSpawnCount()
    {
        defaultSpawnCount++;
        spawnCount = defaultSpawnCount;
    }

    private void IncreaseMultiplier()
    {
        spawnCountMultiplier++;
    }
}
