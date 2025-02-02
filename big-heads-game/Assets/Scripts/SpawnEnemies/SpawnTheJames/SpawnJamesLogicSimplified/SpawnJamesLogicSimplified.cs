using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnJamesLogicSimplified : MonoBehaviour
{
    private Transform playerTransform;
    private List<EnemyConfig> enemyConfigs;

    private float spawnInterval = 1f;
    private int minEnemiesToSpawn = 1;
    private int maxEnemiesToSpawn = 2;

    private float spacingBetweenEnemies = 2f;
    private float distanceFromPlayer = 15f;
    private int totalEnemiesToSpawn = 3;

    private float spawnTimer;
    private float elapsedTime = 0f;

    public SpawnJamesLogicSimplified(Transform playerTransform, List<EnemyConfig> enemyConfigs) {
        this.playerTransform = playerTransform;
        this.enemyConfigs = enemyConfigs;
    }

    public void Update() {
        elapsedTime += Time.deltaTime;

        // Aumenta a dificuldade a cada 30 segundos
        if (elapsedTime > 30f) {
            IncreaseSpawnRate();
            elapsedTime = 0f;
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval) {
            SpawnEnemies();
            spawnTimer = 0f;
        }
    }

    private void IncreaseSpawnRate() {
        minEnemiesToSpawn = Mathf.Min(minEnemiesToSpawn + 1, 5);
        maxEnemiesToSpawn = Mathf.Min(maxEnemiesToSpawn + 2, 12);
        totalEnemiesToSpawn += 3;
        spawnInterval = Mathf.Max(spawnInterval - 0.1f, 0.5f);

        Debug.Log($"Dificuldade aumentada! Min: {minEnemiesToSpawn}, Max: {maxEnemiesToSpawn}, Total: {totalEnemiesToSpawn}");
    }

    private void SpawnEnemies() {
        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

        int direction = Random.Range(0, 4);
        Vector3 startPosition = playerTransform.position;
        Vector3 offsetDirection = Vector3.zero;

        switch (direction) {
            case 0: offsetDirection = Vector3.right; break;
            case 1: offsetDirection = Vector3.left; break;
            case 2: offsetDirection = Vector3.up; break;
            case 3: offsetDirection = Vector3.down; break;
        }

        startPosition += offsetDirection * distanceFromPlayer;

        int enemiesToSpawn = Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn + 1);
        float offset = (enemiesToSpawn - 1) / 2f * spacingBetweenEnemies;

        for (int i = 0; i < enemiesToSpawn; i++) {
            Vector3 spawnPosition = startPosition + (offsetDirection == Vector3.right || offsetDirection == Vector3.left
                ? Vector3.up * (i * spacingBetweenEnemies - offset)
                : Vector3.right * (i * spacingBetweenEnemies - offset));

            GameObject enemyInstance = Object.Instantiate(selectedConfig.enemyPrefab, spawnPosition, Quaternion.identity);
            BaseEnemy enemy = enemyInstance.GetComponent<BaseEnemy>();
            if (enemy != null) {
                enemy.Initialize(selectedConfig);
            }
        }
    }
}
