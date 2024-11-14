using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUniqueManyEnemies : MonoBehaviour
{
    public List<EnemyConfig> enemyConfigs; // Diferentes configurações de inimigos
    public Transform playerTransform; // Referência ao Transform do player
    public float spacingBetweenEnemies = 2f; // Espaçamento entre os inimigos na linha
    public float distanceFromPlayer = 15f; // Distância da linha de inimigos em relação ao player
    public int totalEnemiesToSpawn = 10; // Número total de inimigos para spawnar

    private bool hasSpawned = false; // Verifica se o spawn já foi realizado

    void Start() {
        if (!hasSpawned) {
            SpawnEnemiesInSingleDirection();
            hasSpawned = true;
        }
    }

    void SpawnEnemiesInSingleDirection() {
        // Seleciona uma configuração de inimigo aleatória
        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

        // Escolhe uma direção aleatória (0 = direita, 1 = esquerda, 2 = acima, 3 = abaixo)
        int direction = Random.Range(0, 4);

        // Define a direção e calcula a posição inicial para o spawn
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

        // Ajuste para centralizar o grupo de inimigos em relação ao player
        float offset = (totalEnemiesToSpawn - 1) / 2f * spacingBetweenEnemies;

        for (int i = 0; i < totalEnemiesToSpawn; i++) {
            // Calcula a posição de cada inimigo na linha
            Vector3 spawnPosition = startPosition + (offsetDirection == Vector3.right || offsetDirection == Vector3.left
                ? Vector3.up * (i * spacingBetweenEnemies - offset) // Direita ou Esquerda -> alinhe no eixo Y
                : Vector3.right * (i * spacingBetweenEnemies - offset)); // Acima ou Abaixo -> alinhe no eixo X

            // Instancia o inimigo na posição calculada
            GameObject enemyInstance = Instantiate(selectedConfig.enemyPrefab, spawnPosition, Quaternion.identity);

            // Inicializa o inimigo com o EnemyConfig
            BaseEnemy enemy = enemyInstance.GetComponent<BaseEnemy>();
            if (enemy != null) {
                enemy.Initialize(selectedConfig);
            }
        }
    }
}
