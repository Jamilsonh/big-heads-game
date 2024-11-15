using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUniqueManyEnemisObject : MonoBehaviour, ISpawnTypeObject {
    private SpawnConfigObjects config;
    private Transform playerTransform;
    private List<EnemyConfig> enemyConfigs;
    private bool hasSpawned = false;

    public bool HasFinishedSpawning => hasSpawned;

    public void Initialize(SpawnConfigObjects config, Transform playerTransform, List<EnemyConfig> enemyConfigs) {
        this.config = config;
        this.playerTransform = playerTransform;
        this.enemyConfigs = enemyConfigs;
        hasSpawned = false;
    }

    public void SpawnEnemies() {
        if (hasSpawned) return;

        Vector3 startPosition = playerTransform.position + Vector3.right * config.distanceFromPlayer;
        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];
        float offset = (config.totalEnemiesToSpawn - 1) / 2f * config.spacingBetweenEnemies;

        for (int i = 0; i < config.totalEnemiesToSpawn; i++) {
            Vector3 spawnPosition = startPosition + Vector3.up * (i * config.spacingBetweenEnemies - offset);
            GameObject enemy = Instantiate(selectedConfig.enemyPrefab, spawnPosition, Quaternion.identity);
        }

        hasSpawned = true;
    }
}
