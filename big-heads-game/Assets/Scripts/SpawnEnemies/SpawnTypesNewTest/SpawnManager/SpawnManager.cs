using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform playerTransform;
    public List<EnemyConfig> enemyConfigs;
    public int initialEnemyCount = 60;
    public int enemyIncrement = 30;
    public float blockDuration = 60f;
    public float spawnInterval = 10f;

    private int currentBlockEnemyCount;
    private int enemiesSpawnedInBlock;
    private float blockTimer;
    private float spawnTimer;

    private List<ISpawnType> spawnTypes;
    private int currentSpawnIndex;

    private void Start() {
        currentBlockEnemyCount = initialEnemyCount;
        enemiesSpawnedInBlock = 0;
        blockTimer = blockDuration;
        spawnTimer = spawnInterval;

        // Inicializa os tipos de spawn
        spawnTypes = new List<ISpawnType>
        {
            GetComponent<SpawnUniqueManyEnemiesNew>(),
            GetComponent<SpawnFastSingleNew>()
        };

        foreach (var spawnType in spawnTypes) {
            spawnType.Initialize(playerTransform, enemyConfigs, initialEnemyCount);
        }

        currentSpawnIndex = 0;
        StartNewSpawn();
    }

    private void Update() {
        blockTimer -= Time.deltaTime;

        if (blockTimer <= 0 || enemiesSpawnedInBlock >= currentBlockEnemyCount) {
            // Passa para o próximo bloco
            currentBlockEnemyCount += enemyIncrement;
            enemiesSpawnedInBlock = 0;
            blockTimer = blockDuration;
            StartNewSpawn();
        }
        else {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0) {
                if (spawnTypes[currentSpawnIndex].HasFinishedSpawning) {
                    // Alterna para o próximo tipo de spawn
                    currentSpawnIndex = (currentSpawnIndex + 1) % spawnTypes.Count;
                    StartNewSpawn();
                }
                else {
                    spawnTypes[currentSpawnIndex].SpawnEnemies();
                    spawnTimer = spawnInterval;
                    enemiesSpawnedInBlock += spawnTypes[currentSpawnIndex] is SpawnFastSingle ?
                                              Random.Range(2, 5) : 10;
                }
            }
        }
    }

    private void StartNewSpawn() {
        spawnTypes[currentSpawnIndex].Initialize(playerTransform, enemyConfigs, currentBlockEnemyCount);
    }

}
