using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOneSide : ISpawnStrategy 
{
    private Transform playerTransform;
    private List<EnemyConfig> enemyConfigs;
    private string strategyName;

    private int minEnemiesToSpawn = 3;
    private int maxEnemiesToSpawn = 6;

    private float spacingBetweenEnemies = 2f;
    private float distanceFromPlayer = 15f;

    private bool hasSpawned;

    public SpawnOneSide(Transform playerTransform, List<EnemyConfig> enemyConfigs, string name) {
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
            SpawnEnemiesFromOneSide();
            hasSpawned = true; // Garante que o spawn ocorre apenas uma vez
        }
    }

    public void End() {
        // Não há lógica adicional no término dessa estratégia
    }

    public string GetName() {
        return strategyName;
    }

    private void SpawnEnemiesFromOneSide() {
        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

        // Escolher um lado único para o spawn: direita, esquerda, acima ou abaixo
        int direction = Random.Range(0, 4);

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

        // Determinar o número de inimigos para spawnar
        int enemiesToSpawn = Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn + 1);
        float offset = (enemiesToSpawn - 1) / 2f * spacingBetweenEnemies;

        // Loop para instanciar os inimigos na formação
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
    }
}
