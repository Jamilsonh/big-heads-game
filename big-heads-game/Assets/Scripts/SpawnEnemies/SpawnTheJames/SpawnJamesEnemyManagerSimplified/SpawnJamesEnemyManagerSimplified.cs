using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnJamesEnemyManagerSimplified : MonoBehaviour
{
    public Transform playerTransform;
    public List<EnemyConfig> enemyConfigs;
    public float initialDelay = 5f;

    private SpawnJamesLogicSimplified spawnLogic;
    private bool initialDelayPassed = false;

    void Start() {
        spawnLogic = new SpawnJamesLogicSimplified(playerTransform, enemyConfigs);
    }

    void Update() {
        if (!initialDelayPassed) {
            initialDelay -= Time.deltaTime;
            if (initialDelay <= 0f) {
                initialDelayPassed = true;
            }
            return; 
        }

        spawnLogic.Update();
    }
}
