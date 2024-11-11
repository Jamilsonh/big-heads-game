using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyConfig> enemyConfigs; // Adicione os diferentes EnemyConfig
    public Transform[] spawnPoints; // Posições ao redor da tela para o spawn 
    public float spawnInterval = 2f;

    private float spawnTimer;

    void Update() {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval) {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemy() {
        // Seleciona um EnemyConfig aleatoriamente
        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

        // Seleciona um ponto de spawn aleatório
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instancia o prefab do inimigo configurado no EnemyConfig
        GameObject enemyInstance = Instantiate(selectedConfig.enemyPrefab, spawnPoint.position, Quaternion.identity);

        // Inicializa o inimigo com o EnemyConfig
        BaseEnemy enemy = enemyInstance.GetComponent<BaseEnemy>();
        if (enemy != null) {
            enemy.Initialize(selectedConfig);
        }
    }











}
