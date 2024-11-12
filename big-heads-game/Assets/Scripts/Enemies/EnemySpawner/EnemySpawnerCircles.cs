using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerCircles : MonoBehaviour
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
            SpawnEnemiesAtOppositeSide();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemiesAtOppositeSide() {
        // Definir limites do spawn com base na posição do player
        float startAngle, endAngle;

        if (player.position.y > mapTopBoundary - spawnRadius) {
            // Jogador está próximo ao limite superior - spawn inimigos na parte inferior
            startAngle = 180f;
            endAngle = 360f;
        }
        else if (player.position.y < mapBottomBoundary + spawnRadius) {
            // Jogador está próximo ao limite inferior - spawn inimigos na parte superior
            startAngle = 0f;
            endAngle = 180f;
        }
        else if (player.position.x < mapLeftBoundary + spawnRadius) {
            // Jogador está próximo ao limite esquerdo - spawn inimigos na parte direita
            startAngle = -90f;
            endAngle = 90f;
        }
        else if (player.position.x > mapRightBoundary - spawnRadius) {
            // Jogador está próximo ao limite direito - spawn inimigos na parte esquerda
            startAngle = 90f;
            endAngle = 270f;
        }
        else {
            // Jogador está no centro do mapa - spawn inimigos em círculo completo
            startAngle = 0f;
            endAngle = 360f;
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
