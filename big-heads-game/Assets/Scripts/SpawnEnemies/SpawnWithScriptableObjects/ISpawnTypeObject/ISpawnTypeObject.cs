using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnTypeObject
{
    void Initialize(SpawnConfigObjects config, Transform playerTransform, List<EnemyConfig> enemyConfigs);
    void SpawnEnemies();
    bool HasFinishedSpawning { get; }
}
