using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTwoSides : ISpawnStrategy
{
    private Transform playerTransform;
    private List<EnemyConfig> enemyConfigs;
    private string strategyName;

    private int minEnemiesPerSide = 3;
    private int maxEnemiesPerSide = 6;

    private float spacingBetweenEnemies = 2f;
    private float distanceFromPlayer = 15f;

    private bool hasSpawned;

    public SpawnTwoSides(Transform playerTransform, List<EnemyConfig> enemyConfigs, string name) {
        this.playerTransform = playerTransform; // Define a posição do jogador
        this.enemyConfigs = enemyConfigs; // Lista de inimigos disponíveis
        this.strategyName = name; // Nome da estratégia
        this.hasSpawned = false; // Controle para spawn único
    }

    public void Start() {
        hasSpawned = false; // Reseta o estado para permitir o spawn único
    }

    public void Update() {
        if (!hasSpawned) {
            SpawnEnemiesFromTwoSides();
            hasSpawned = true; // Garante que o spawn ocorre apenas uma vez
        }
    }

    public void End() {
        // Não há lógica adicional no término dessa estratégia
    }

    public string GetName() {
        return strategyName;
    }

    private void SpawnEnemiesFromTwoSides() {
        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

        // Escolher dois lados distintos para o spawn
        int side1 = Random.Range(0, 4);
        int side2;
        do {
            side2 = Random.Range(0, 4);
        } while (side2 == side1); // Garante que os lados sejam diferentes

        // Gerar inimigos em ambos os lados
        SpawnEnemiesOnSide(selectedConfig, side1);
        SpawnEnemiesOnSide(selectedConfig, side2);
    }

    private void SpawnEnemiesOnSide(EnemyConfig config, int direction) {
        Vector3 startPosition = playerTransform.position;
        Vector3 offsetDirection = Vector3.zero;

        switch (direction) {
            case 0: // Direita
                offsetDirection = Vector3.right;
                startPosition += offsetDirection * distanceFromPlayer;
                break;
            case 1: // Esquerda
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

        // Determinar o número de inimigos para spawnar nesse lado
        int enemiesToSpawn = Random.Range(minEnemiesPerSide, maxEnemiesPerSide + 1);
        float offset = (enemiesToSpawn - 1) / 2f * spacingBetweenEnemies;

        // Loop para instanciar os inimigos na formação
        for (int i = 0; i < enemiesToSpawn; i++) {
            Vector3 spawnPosition = startPosition + (offsetDirection == Vector3.right || offsetDirection == Vector3.left
                ? Vector3.up * (i * spacingBetweenEnemies - offset)
                : Vector3.right * (i * spacingBetweenEnemies - offset));

            GameObject enemyInstance = Object.Instantiate(config.enemyPrefab, spawnPosition, Quaternion.identity);

            BaseEnemy enemy = enemyInstance.GetComponent<BaseEnemy>();
            if (enemy != null) {
                enemy.Initialize(config);
            }
        }
    }
}
