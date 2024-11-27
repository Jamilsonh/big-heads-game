using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCircle : ISpawnStrategy {
    private Transform playerTransform;
    private List<EnemyConfig> enemyConfigs;
    private string strategyName;

    private int enemiesToSpawn = 15; // N�mero fixo de inimigos
    private float radius = 10f;

    private bool hasSpawned;

    public SpawnCircle(Transform playerTransform, List<EnemyConfig> enemyConfigs, string name) {
        this.playerTransform = playerTransform; // Define a posi��o do jogador
        this.enemyConfigs = enemyConfigs; // Lista de inimigos dispon�veis
        this.strategyName = name; // Nome da estrat�gia
        this.hasSpawned = false; // Controle para spawn �nico
    }

    public void Start() {
        hasSpawned = false; // Reseta o estado para permitir o spawn �nico
    }

    public void Update() {
        if (!hasSpawned) {
            SpawnEnemiesInCircle();
            hasSpawned = true; // Garante que o spawn ocorre apenas uma vez
        }
    }

    public void End() {
        // N�o h� l�gica adicional no t�rmino dessa estrat�gia
    }

    public string GetName() {
        return strategyName;
    }

    private void SpawnEnemiesInCircle() {
        // Selecionar a configura��o de inimigo aleatoriamente
        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

        // �ngulo entre cada inimigo no c�rculo
        float angleStep = 360f / enemiesToSpawn;

        for (int i = 0; i < enemiesToSpawn; i++) {
            // Calcular a posi��o de cada inimigo no c�rculo
            float angle = i * angleStep;
            float radians = angle * Mathf.Deg2Rad;

            Vector3 spawnPosition = new Vector3(
                playerTransform.position.x + Mathf.Cos(radians) * radius,
                playerTransform.position.y + Mathf.Sin(radians) * radius,
                0f // Z permanece constante
            );

            // Instanciar o inimigo na posi��o calculada
            GameObject enemyInstance = Object.Instantiate(selectedConfig.enemyPrefab, spawnPosition, Quaternion.identity);

            BaseEnemy enemy = enemyInstance.GetComponent<BaseEnemy>();
            if (enemy != null) {
                enemy.Initialize(selectedConfig);
            }
        }
    }
}
