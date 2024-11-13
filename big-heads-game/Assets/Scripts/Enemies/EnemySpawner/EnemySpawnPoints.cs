using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoints : MonoBehaviour
{
    public List<EnemyConfig> enemyConfigs; // Diferentes configura��es de inimigos
    public Transform playerTransform; // Refer�ncia ao Transform do player
    public float spawnInterval = 2f;
    public int minEnemiesToSpawn = 3; // Quantidade m�nima de inimigos a spawnar
    public int maxEnemiesToSpawn = 7; // Quantidade m�xima de inimigos a spawnar
    public float spacingBetweenEnemies = 2f; // Espa�amento entre os inimigos na linha
    public float distanceFromPlayer = 3f; // Dist�ncia da linha de inimigos em rela��o ao player

    private float spawnTimer;

    void Update() {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval) {
            SpawnEnemiesInRandomDirection();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemiesInRandomDirection() {
        // Seleciona uma configura��o de inimigo aleat�ria
        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

        // Escolhe uma dire��o aleat�ria (0 = direita, 1 = esquerda, 2 = acima, 3 = abaixo)
        int direction = Random.Range(0, 4);

        // Define a quantidade de inimigos a ser spawnada aleatoriamente entre o m�nimo e o m�ximo
        int enemiesToSpawn = Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn + 1);

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
    }
}
