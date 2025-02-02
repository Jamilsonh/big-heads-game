using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLogicSimplified : ISpawnStrategySimplified
{
    private Transform playerTransform;
    private List<EnemyConfig> enemyConfigs;
    private string strategyName;

    private float spawnInterval = 1f;
    private int minEnemiesToSpawn = 1;
    private int maxEnemiesToSpawn = 2;

    private float spacingBetweenEnemies = 2f;
    private float distanceFromPlayer = 15f;
    private int totalEnemiesToSpawn = 3;

    private float spawnTimer;
    private int spawnedEnemiesCount;
    private float elapsedTime = 0f; // Tempo de jogo decorrido

    public SpawnLogicSimplified(Transform playerTransform, List<EnemyConfig> enemyConfigs, string name) {
        this.playerTransform = playerTransform;
        this.enemyConfigs = enemyConfigs;
        this.strategyName = name;
    }

    public void Start() {
        spawnTimer = 0f;
        spawnedEnemiesCount = 0;
        elapsedTime = 0f;
    }

    public void Update() {
        elapsedTime += Time.deltaTime;

        // A cada 30 segundos, aumenta progressivamente a dificuldade
        if (elapsedTime > 30f) {
            IncreaseSpawnRate();
            elapsedTime = 0f; // Reseta o timer para a próxima fase de aumento
        }

        if (spawnedEnemiesCount < totalEnemiesToSpawn) {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval) {
                SpawnEnemiesInSingleDirection();
                spawnTimer = 0f;
            }
        }
    }

    public void End() { }

    public string GetName() {
        return strategyName;
    }

    private void IncreaseSpawnRate() {
        // Aumenta a quantidade mínima e máxima de inimigos spawnados ao longo do tempo
        minEnemiesToSpawn = Mathf.Min(minEnemiesToSpawn + 1, 5); // Máximo de 5 para evitar exagero
        maxEnemiesToSpawn = Mathf.Min(maxEnemiesToSpawn + 2, 12); // Máximo de 12 para equilibrar

        totalEnemiesToSpawn += 3; // Aumenta o total de inimigos ao longo do tempo
        spawnInterval = Mathf.Max(spawnInterval - 0.1f, 0.5f); // Diminui o tempo entre os spawns

        Debug.Log($"Spawn rate increased! Min: {minEnemiesToSpawn}, Max: {maxEnemiesToSpawn}, Total: {totalEnemiesToSpawn}");
    }

    private void SpawnEnemiesInSingleDirection() {
        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

        int direction = Random.Range(0, 4);

        int remainingEnemies = totalEnemiesToSpawn - spawnedEnemiesCount;
        int enemiesToSpawn = Mathf.Min(Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn + 1), remainingEnemies);

        Vector3 startPosition = playerTransform.position;
        Vector3 offsetDirection = Vector3.zero;

        switch (direction) {
            case 0: offsetDirection = Vector3.right; break;
            case 1: offsetDirection = Vector3.left; break;
            case 2: offsetDirection = Vector3.up; break;
            case 3: offsetDirection = Vector3.down; break;
        }

        startPosition += offsetDirection * distanceFromPlayer;

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
        spawnedEnemiesCount += enemiesToSpawn;
    }
}
