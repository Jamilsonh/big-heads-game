using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCircle : ISpawnStrategy {
    private Transform playerTransform;
    private List<EnemyConfig> enemyConfigs;
    private string strategyName;

    private int enemiesToSpawn = 15; // Número fixo de inimigos
    private float radius = 10f;

    private bool hasSpawned;

    public SpawnCircle(Transform playerTransform, List<EnemyConfig> enemyConfigs, string name) {
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
            SpawnEnemiesInCircle();
            hasSpawned = true; // Garante que o spawn ocorre apenas uma vez
        }
    }

    public void End() {
        // Não há lógica adicional no término dessa estratégia
    }

    public string GetName() {
        return strategyName;
    }

    private void SpawnEnemiesInCircle() {
        // Selecionar a configuração de inimigo aleatoriamente
        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

        // Ângulo entre cada inimigo no círculo
        float angleStep = 360f / enemiesToSpawn;

        for (int i = 0; i < enemiesToSpawn; i++) {
            // Calcular a posição de cada inimigo no círculo
            float angle = i * angleStep;
            float radians = angle * Mathf.Deg2Rad;

            Vector3 spawnPosition = new Vector3(
                playerTransform.position.x + Mathf.Cos(radians) * radius,
                playerTransform.position.y + Mathf.Sin(radians) * radius,
                0f // Z permanece constante
            );

            // Instanciar o inimigo na posição calculada
            GameObject enemyInstance = Object.Instantiate(selectedConfig.enemyPrefab, spawnPosition, Quaternion.identity);

            BaseEnemy enemy = enemyInstance.GetComponent<BaseEnemy>();
            if (enemy != null) {
                enemy.Initialize(selectedConfig);
            }
        }
    }
}
