using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerQuadrantes : MonoBehaviour
{
    public List<EnemyConfig> enemyConfigs;
    public Transform player;
    public float spawnRadius = 10f;
    public float spawnInterval = 2f;
    public int numberOfEnemies = 8;

    public float mapTopBoundary = 50f;
    public float mapBottomBoundary = -50f;
    public float mapLeftBoundary = -50f;
    public float mapRightBoundary = 50f;

    private float spawnTimer;

    void Update() {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval) {
            SpawnEnemiesInOppositeQuadrant();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemiesInOppositeQuadrant() {
        float startAngle, endAngle;

        // Verifica a posição do jogador para definir o quadrante oposto
        if (player.position.y > (mapTopBoundary + mapBottomBoundary) / 2) {
            if (player.position.x > (mapLeftBoundary + mapRightBoundary) / 2) {
                // Jogador no canto superior direito - spawn no canto inferior esquerdo
                startAngle = 135f;
                endAngle = 225f;
            }
            else {
                // Jogador no canto superior esquerdo - spawn no canto inferior direito
                startAngle = -45f;
                endAngle = 45f;
            }
        }
        else {
            if (player.position.x > (mapLeftBoundary + mapRightBoundary) / 2) {
                // Jogador no canto inferior direito - spawn no canto superior esquerdo
                startAngle = 45f;
                endAngle = 135f;
            }
            else {
                // Jogador no canto inferior esquerdo - spawn no canto superior direito
                startAngle = 225f;
                endAngle = 315f;
            }
        }

        float angleStep = (endAngle - startAngle) / numberOfEnemies;

        for (int i = 0; i < numberOfEnemies; i++) {
            float angle = (startAngle + i * angleStep) * Mathf.Deg2Rad;

            Vector3 spawnPosition = new Vector3(
                player.position.x + Mathf.Cos(angle) * spawnRadius,
                player.position.y + Mathf.Sin(angle) * spawnRadius,
                player.position.z
            );

            EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];
            GameObject enemyInstance = Instantiate(selectedConfig.enemyPrefab, spawnPosition, Quaternion.identity);

            BaseEnemy enemy = enemyInstance.GetComponent<BaseEnemy>();
            if (enemy != null) {
                enemy.Initialize(selectedConfig);
            }
        }
    }
}
