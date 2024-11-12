using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public List<EnemyConfig> enemyConfigs; // Configurações dos inimigos
    public Transform player; // Referência ao Player
    public float spawnRadius = 10f; // Distância ao redor do player
    public float spawnInterval = 2f;
    public int numberOfEnemies = 8; // Número de inimigos para spawnar no círculo

    private float spawnTimer;

    void Update() {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval) {
            SpawnEnemiesAroundPlayer();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemiesAroundPlayer() {
        // Calcular ângulo entre cada inimigo para cobrir apenas metade do círculo (180 graus)
        float angleStep = 180f / (numberOfEnemies - 1);

        for (int i = 0; i < numberOfEnemies; i++) {
            // Calcula o ângulo em radianos, iniciando de 180 graus e indo até 360 graus
            float angle = (180f + i * angleStep) * Mathf.Deg2Rad;

            // Calcula a posição no semicírculo abaixo do player no plano X-Y
            Vector3 spawnPosition = new Vector3(
                player.position.x + Mathf.Cos(angle) * spawnRadius,
                player.position.y + Mathf.Sin(angle) * spawnRadius,
                player.position.z // Mantendo Z fixo
            );

            // Seleciona um EnemyConfig aleatoriamente
            EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

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
