using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUniqueManyEnemiesNew : MonoBehaviour, ISpawnType {
    public List<EnemyConfig> enemyConfigs;
    public Transform playerTransform;
    public float spacingBetweenEnemies = 2f;
    public float distanceFormPlayer = 15f;
    public int totalEnemiesToSpawn = 10;

    private bool hasSpawned = false;
    public bool HasFinishedSpawning => hasSpawned;

    public void Initialize(Transform playerTransform, List<EnemyConfig> enemyConfigs, int totalEnemiesToSpawn) {
        this.playerTransform = playerTransform;
        this.enemyConfigs = enemyConfigs;
        this.totalEnemiesToSpawn = totalEnemiesToSpawn;
        hasSpawned = false;
    }

    public void SpawnEnemies() {
        if (hasSpawned) return;

        // ... [código de spawn conforme acima] ...

        hasSpawned = true;
    }
}
