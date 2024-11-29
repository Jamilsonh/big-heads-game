using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHalfCircle : ISpawnStrategy
{
    private Transform playerTransform;
    private List<EnemyConfig> enemyConfigs;
    private string strategyName;

    private int enemiesToSpawn = 8; // Número fixo de inimigos no meio círculo
    private float radius = 10f;

    private bool hasSpawned;

    public SpawnHalfCircle(Transform playerTransform, List<EnemyConfig> enemyConfigs, string name) {
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
            SpawnEnemiesInHalfCircle();
            hasSpawned = true; // Garante que o spawn ocorre apenas uma vez
        }
    }

    public void End() {
        // Não há lógica adicional no término dessa estratégia
    }

    public string GetName() {
        return strategyName;
    }

    private void SpawnEnemiesInHalfCircle() {
        // Selecionar a configuração de inimigo aleatoriamente
        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

        // Escolher aleatoriamente o lado para o meio círculo (0 = cima, 1 = direita, 2 = baixo, 3 = esquerda)
        int side = Random.Range(0, 4);

        // Definir o ângulo inicial e final com base no lado escolhido
        float startAngle = 0f;
        float endAngle = 180f;

        switch (side) {
            case 0: // Cima
                startAngle = 0f;
                endAngle = 180f;
                break;
            case 1: // Direita
                startAngle = 270f;
                endAngle = 450f;
                break;
            case 2: // Baixo
                startAngle = 180f;
                endAngle = 360f;
                break;
            case 3: // Esquerda
                startAngle = 90f;
                endAngle = 270f;
                break;
        }

        // Ângulo entre cada inimigo no meio círculo
        float angleStep = (endAngle - startAngle) / (enemiesToSpawn - 1);

        for (int i = 0; i < enemiesToSpawn; i++) {
            // Calcular o ângulo para cada inimigo
            float angle = startAngle + (i * angleStep);
            float radians = angle * Mathf.Deg2Rad;

            // Calcular a posição de spawn com base no ângulo e no raio
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
