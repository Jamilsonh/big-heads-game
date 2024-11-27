using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMultiSides : ISpawnStrategy
{
    private Transform playerTransform;
    private List<EnemyConfig> enemyConfigs;
    private string strategyName;

    private float spawnInterval = 0.5f;
    private int minEnemiesToSpawn = 2;
    private int maxEnemiesToSpawn = 5;

    private float spacingBetweenEnemies = 2f;
    private float distanceFromPlayer = 15f;
    private int totalEnemiesToSpawn = 10;

    private float spawnTimer;
    private int spawnedEnemiesCount;

    public SpawnMultiSides(Transform playerTransform, List<EnemyConfig> enemyConfigs, string name) {
        this.playerTransform = playerTransform; // Define a posição do jogador
        this.enemyConfigs = enemyConfigs; // Lista de inimigos disponíveis
        this.strategyName = name; // Define o nome da estratégia
    }

    public void Start() {
        spawnTimer = 0f;
        spawnedEnemiesCount = 0;
    }

    public void Update() {
        if (spawnedEnemiesCount < totalEnemiesToSpawn) {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval) {
                SpawnEnemiesInSingleDirection();
                spawnTimer = 0f;
            }
        }
    }

    public void End() {

    }

    public string GetName() {
        return strategyName;
    }

    private void SpawnEnemiesInSingleDirection() {

        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

        int direction = Random.Range(0, 4);

        int remainingEnemies = totalEnemiesToSpawn - spawnedEnemiesCount;
        int enemiesToSpawn = Mathf.Min(Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn + 1), remainingEnemies);

        Vector3 startPosition = playerTransform.position;
        Vector3 offsetDirection = Vector3.zero;

        switch (direction) {
            case 0: // Direita
                offsetDirection = Vector3.right;
                startPosition += offsetDirection * distanceFromPlayer;
                break;
            case 1:
                offsetDirection = Vector3.left;
                startPosition += offsetDirection * distanceFromPlayer;
                break;
            case 2: // Acima
                offsetDirection = Vector3.up;
                startPosition += offsetDirection * distanceFromPlayer;
                break;
            case 3: // Abaixo
                offsetDirection = Vector3.down;
                startPosition += offsetDirection * distanceFromPlayer;
                break;
        }

        float offset = (enemiesToSpawn - 1) / 2f * spacingBetweenEnemies;

        // Loop para instanciar os inimigos na formação
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
        spawnedEnemiesCount += enemiesToSpawn;
    }
}
