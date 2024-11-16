using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnFastSingle : MonoBehaviour
{
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

    void OnEnable() {
        // Reinicia os contadores ao ativar a estrat�gia
        spawnTimer = 0f;
        spawnedEnemiesCount = 0;
    }

    void Update() {
        if (spawnedEnemiesCount < totalEnemiesToSpawn) {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval) {
                SpawnEnemiesInSingleDirection();
                spawnTimer = 0f;
            }
        }
    }

    void SpawnEnemiesInSingleDirection() {
        // Seleciona uma configura��o de inimigo aleat�ria
        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

        // Escolhe uma dire��o aleat�ria (0 = direita, 1 = esquerda, 2 = acima, 3 = abaixo)
        int direction = Random.Range(0, 4);

        // Define a quantidade de inimigos a ser spawnada aleatoriamente entre o m�nimo e o m�ximo,
        // respeitando o total de inimigos restantes
        int remainingEnemies = totalEnemiesToSpawn - spawnedEnemiesCount;
        int enemiesToSpawn = Mathf.Min(Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn + 1), remainingEnemies);

        // Calcula a posi��o inicial para o spawn baseada na dire��o escolhida
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

        // Ajuste para centralizar o grupo de inimigos em rela��o ao player
        float offset = (enemiesToSpawn - 1) / 2f * spacingBetweenEnemies;

        for (int i = 0; i < enemiesToSpawn; i++) {
            // Calcula a posi��o de cada inimigo na linha
            Vector3 spawnPosition = startPosition + (offsetDirection == Vector3.right || offsetDirection == Vector3.left
                ? Vector3.up * (i * spacingBetweenEnemies - offset) // Direita ou Esquerda -> alinhe no eixo Y
                : Vector3.right * (i * spacingBetweenEnemies - offset)); // Acima ou Abaixo -> alinhe no eixo X

            // Instancia o inimigo na posi��o calculada
            GameObject enemyInstance = Instantiate(selectedConfig.enemyPrefab, spawnPosition, Quaternion.identity);

            // Inicializa o inimigo com o EnemyConfig
            BaseEnemy enemy = enemyInstance.GetComponent<BaseEnemy>();
            if (enemy != null) {
                enemy.Initialize(selectedConfig);
            }
        }

        // Atualiza o contador de inimigos j� spawnados
        spawnedEnemiesCount += enemiesToSpawn;
    }
}