using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUniqueManyEnemys : MonoBehaviour
{
    private Transform playerTransform; 
    private List<EnemyConfig> enemyConfigs; 
    private string strategyName;
    private float spacingBetweenEnemies = 2f;
    private float distanceFromPlayer = 15f;
    private int totalEnemiesToSpawn = 10;
    private bool hasSpawned;

    public SpawnUniqueManyEnemys(Transform playerTransform, List<EnemyConfig> enemyConfigs, string name) {
        this.playerTransform = playerTransform; 
        this.enemyConfigs = enemyConfigs; 
        this.strategyName = name;
    }

    public void Start() {
        hasSpawned = false;
    }

    public void Update() {
        if (!hasSpawned) {
            SpawnEnemiesInSingleDirection();
            hasSpawned = true;
        }
    }

    public void End() {
        
    }

    public string GetName() {
        return strategyName;
    }

    private void SpawnEnemiesInSingleDirection() {
        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

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

        float offset = (totalEnemiesToSpawn - 1) / 2f * spacingBetweenEnemies;

        for (int i = 0; i < totalEnemiesToSpawn; i++) {
            Vector3 spawnPosition = startPosition + (offsetDirection == Vector3.right || offsetDirection == Vector3.left
                ? Vector3.up * (i * spacingBetweenEnemies - offset) // Caso seja Direita ou Esquerda, ajuste no eixo Y
                : Vector3.right * (i * spacingBetweenEnemies - offset)); // Caso seja Acima ou Abaixo, ajuste no eixo X

            GameObject enemyInstance = Object.Instantiate(selectedConfig.enemyPrefab, spawnPosition, Quaternion.identity);

            BaseEnemy enemy = enemyInstance.GetComponent<BaseEnemy>();
            if (enemy != null) {
                enemy.Initialize(selectedConfig); // Aplica a configuração ao inimigo instanciado
            }
        }
    }
}
