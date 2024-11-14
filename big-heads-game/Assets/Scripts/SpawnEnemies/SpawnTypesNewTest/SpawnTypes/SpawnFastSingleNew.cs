using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFastSingleNew : MonoBehaviour, ISpawnType {
    public List<EnemyConfig> enemyConfigs;
    public Transform playerTransform;
    public float spawnInterval = 0.5f;
    public int minEnemiesToSpawn = 2;
    public int maxEnemiesToSpawn = 5;
    public float spacingBetweenEnemies = 2f;
    public float distanceFromPlayer = 15f;
    public int totalEnemiesToSpawn = 10;

    private float spawnTimer;
    private int spawnedEnemiesCount;
    public bool HasFinishedSpawning => spawnedEnemiesCount >= totalEnemiesToSpawn;

    public void Initialize(Transform playerTransform, List<EnemyConfig> enemyConfigs, int totalEnemiesToSpawn) {
        this.playerTransform = playerTransform;
        this.enemyConfigs = enemyConfigs;
        this.totalEnemiesToSpawn = totalEnemiesToSpawn;
        spawnedEnemiesCount = 0;
        spawnTimer = 0f;
    }

    public void SpawnEnemies() {
        // ... [código de spawn conforme acima] ...
    }
}
