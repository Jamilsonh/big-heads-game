using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQuarterCircle : ISpawnStrategy
{
    private Transform playerTransform;
    private List<EnemyConfig> enemyConfigs;
    private string strategyName;

    private int enemiesToSpawn = 5; // N�mero fixo de inimigos no 1/4 de c�rculo
    private float radius = 10f;

    private bool hasSpawned;

    public SpawnQuarterCircle(Transform playerTransform, List<EnemyConfig> enemyConfigs, string name) {
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
            SpawnEnemiesInQuarterCircle();
            hasSpawned = true; // Garante que o spawn ocorre apenas uma vez
        }
    }

    public void End() {
        // N�o h� l�gica adicional no t�rmino dessa estrat�gia
    }

    public string GetName() {
        return strategyName;
    }

    private void SpawnEnemiesInQuarterCircle() {
        // Selecionar a configura��o de inimigo aleatoriamente
        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

        // Escolher aleatoriamente o quadrante para o 1/4 de c�rculo (0 = cima-direita, 1 = direita-baixo, 2 = baixo-esquerda, 3 = esquerda-cima)
        int quadrant = Random.Range(0, 4);

        // Definir o �ngulo inicial e final com base no quadrante escolhido
        float startAngle = 0f;
        float endAngle = 90f;

        switch (quadrant) {
            case 0: // Cima-direita
                startAngle = 0f;
                endAngle = 90f;
                break;
            case 1: // Direita-baixo
                startAngle = 90f;
                endAngle = 180f;
                break;
            case 2: // Baixo-esquerda
                startAngle = 180f;
                endAngle = 270f;
                break;
            case 3: // Esquerda-cima
                startAngle = 270f;
                endAngle = 360f;
                break;
        }

        // �ngulo entre cada inimigo no 1/4 de c�rculo
        float angleStep = (endAngle - startAngle) / (enemiesToSpawn - 1);

        for (int i = 0; i < enemiesToSpawn; i++) {
            // Calcular o �ngulo para cada inimigo
            float angle = startAngle + (i * angleStep);
            float radians = angle * Mathf.Deg2Rad;

            // Calcular a posi��o de spawn com base no �ngulo e no raio
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
