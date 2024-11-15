using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFastSingleObject : MonoBehaviour, ISpawnTypeObject {
    private SpawnConfigObjects config;
    private Transform playerTransform;
    private List<EnemyConfig> enemyConfigs;
    private float spawnTimer = 0f;
    private int spawnedEnemies = 0;

    public bool HasFinishedSpawning => spawnedEnemies >= config.totalEnemiesToSpawn;

    public void Initialize(SpawnConfigObjects config, Transform playerTransform, List<EnemyConfig> enemyConfigs) {
        this.config = config;
        this.playerTransform = playerTransform;
        this.enemyConfigs = enemyConfigs;
        spawnTimer = 0f;
        spawnedEnemies = 0;
    }

    public void SpawnEnemies() {
        if (HasFinishedSpawning) return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= config.spawnInterval) {
            Vector3 startPosition = playerTransform.position + Vector3.right * config.distanceFromPlayer;
            EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];
            GameObject enemy = Instantiate(selectedConfig.enemyPrefab, startPosition, Quaternion.identity);
            spawnTimer = 0f;
            spawnedEnemies++;
        }
    }
}
