using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public Transform playerTransform; // Referência ao Transform do jogador
    public List<EnemyConfig> enemyConfigs; // Configurações para tipos de inimigos

    public int initialEnemyCount = 60; // Número de inimigos no primeiro bloco
    public int enemyIncrement = 30; // Quantidade de inimigos adicionada em cada bloco
    public float blockDuration = 60f; // Duração de cada bloco (em segundos)
    public float spawnInterval = 10f; // Intervalo entre cada spawn dentro de um bloco

    private int currentBlockEnemyCount; // Número atual de inimigos a spawnar no bloco atual
    private int enemiesSpawnedInBlock; // Quantidade de inimigos já spawnados no bloco atual
    private float blockTimer; // Temporizador para duração do bloco
    private float spawnTimer; // Temporizador para o intervalo entre spawns

    private List<ISpawnType> spawnTypes; // Lista que armazena diferentes tipos de spawn
    private int currentSpawnIndex; // Índice do tipo de spawn atualmente em uso

    private void Start() {
        Debug.Log("Inicializando SpawnManager...");

        currentBlockEnemyCount = initialEnemyCount;
        Debug.Log($"Configuração inicial: currentBlockEnemyCount = {currentBlockEnemyCount}");

        enemiesSpawnedInBlock = 0;
        blockTimer = blockDuration;
        spawnTimer = spawnInterval;

        spawnTypes = new List<ISpawnType>
        {
            GetComponent<SpawnUniqueManyEnemiesNew>(),
            GetComponent<SpawnFastSingleNew>()
        };

        foreach (var spawnType in spawnTypes) {
            spawnType.Initialize(playerTransform, enemyConfigs, initialEnemyCount);
            Debug.Log($"Tipo de spawn inicializado: {spawnType.GetType().Name}");
        }

        currentSpawnIndex = 0;
        Debug.Log($"Tipo de spawn inicial: {spawnTypes[currentSpawnIndex].GetType().Name}");

        StartNewSpawn();
    }

    private void Update() {
        blockTimer -= Time.deltaTime;
        Debug.Log($"blockTimer: {blockTimer:F2}");

        if (blockTimer <= 0 || enemiesSpawnedInBlock >= currentBlockEnemyCount) {
            Debug.Log("Bloco atual finalizado. Preparando próximo bloco...");
            currentBlockEnemyCount += enemyIncrement;
            Debug.Log($"Aumentando inimigos no próximo bloco: currentBlockEnemyCount = {currentBlockEnemyCount}");

            enemiesSpawnedInBlock = 0;
            blockTimer = blockDuration;

            StartNewSpawn();
        }
        else {
            spawnTimer -= Time.deltaTime;
            Debug.Log($"spawnTimer: {spawnTimer:F2}");

            if (spawnTimer <= 0) {
                if (spawnTypes[currentSpawnIndex].HasFinishedSpawning) {
                    currentSpawnIndex = (currentSpawnIndex + 1) % spawnTypes.Count;
                    Debug.Log($"Mudando para o próximo tipo de spawn: {spawnTypes[currentSpawnIndex].GetType().Name}");

                    StartNewSpawn();
                }
                else {
                    Debug.Log($"Executando spawn de inimigos com {spawnTypes[currentSpawnIndex].GetType().Name}");
                    spawnTypes[currentSpawnIndex].SpawnEnemies();
                    spawnTimer = spawnInterval;

                    int enemiesSpawned = spawnTypes[currentSpawnIndex] is SpawnFastSingle ? Random.Range(2, 5) : 10;
                    enemiesSpawnedInBlock += enemiesSpawned;
                    Debug.Log($"Inimigos spawnados: {enemiesSpawned}, Total no bloco: {enemiesSpawnedInBlock}");
                }
            }
        }
    }

    private void StartNewSpawn() {
        Debug.Log($"Inicializando tipo de spawn: {spawnTypes[currentSpawnIndex].GetType().Name}");
        spawnTypes[currentSpawnIndex].Initialize(playerTransform, enemyConfigs, currentBlockEnemyCount);
    }
}
