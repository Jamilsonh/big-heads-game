using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnType {
    void Initialize(Transform playerTransform, List<EnemyConfig> enemyConfigs, int totalEnemiesToSpawn);
    void SpawnEnemies();
    bool HasFinishedSpawning { get; }
}
