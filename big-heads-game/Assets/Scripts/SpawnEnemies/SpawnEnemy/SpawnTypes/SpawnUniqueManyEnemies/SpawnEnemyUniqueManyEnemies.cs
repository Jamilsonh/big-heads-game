using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyUniqueManyEnemies : ISpawnStrategy
{
    private Transform playerTransform; // Refer�ncia � posi��o do jogador, usada como base para calcular o spawn dos inimigos
    private List<EnemyConfig> enemyConfigs; // Lista de configura��es de inimigos que podem ser spawnados
    private string name; // Nome da estrat�gia de spawn, �til para identifica��o
    private float spacingBetweenEnemies = 2f; // Espa�amento entre inimigos ao spawnar em linha  
    private float distanceFromPlayer = 15f; // Dist�ncia inicial entre o jogador e o local onde os inimigos v�o spawnar  
    private int totalEnemiesToSpawn = 10; // Quantidade total de inimigos a ser spawnada
    private bool hasSpawned; // Flag para verificar se os inimigos j� foram spawnados

    // Construtor para inicializar os par�metros da estrat�gia
    public SpawnEnemyUniqueManyEnemies(Transform playerTransform, List<EnemyConfig> enemyConfigs, string name) {
        this.playerTransform = playerTransform; // Define a posi��o do jogador
        this.enemyConfigs = enemyConfigs; // Lista de inimigos dispon�veis
        this.name = name; // Define o nome da estrat�gia
    }

    // M�todo chamado quando a estrat�gia � iniciada
    public void Start() {
        hasSpawned = false; // Reseta a flag de spawn para falso
    }

    // M�todo chamado a cada frame para atualizar a estrat�gia
    public void Update() {
        if (!hasSpawned) {
            SpawnEnemiesInSingleDirection(); // Realiza o spawn dos inimigos em uma dire��o
            hasSpawned = true; // Marca como conclu�do para evitar respawns repetidos
        }
    }

    // M�todo chamado quando a estrat�gia termina (implementa��o futura, se necess�rio)
    public void End() {
        // Limpeza ou finaliza��o de recursos, se necess�rio
    }

    // Retorna o nome da estrat�gia
    public string GetName() {
        return name;
    }

    // M�todo privado que gerencia o spawn dos inimigos em uma �nica dire��o
    private void SpawnEnemiesInSingleDirection() {
        // Seleciona uma configura��o de inimigo aleat�ria da lista
        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

        // Determina uma dire��o aleat�ria: direita, esquerda, acima ou abaixo do jogador
        int direction = Random.Range(0, 4);

        // Calcula a posi��o inicial de spawn com base na dire��o selecionada
        Vector3 startPosition = playerTransform.position;
        Vector3 offsetDirection = Vector3.zero; // Vetor que representa a dire��o do spawn

        // Determina a dire��o e ajusta a posi��o inicial
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

        // Calcula o deslocamento para centralizar a linha de inimigos
        float offset = (totalEnemiesToSpawn - 1) / 2f * spacingBetweenEnemies;

        // Loop para spawnar cada inimigo na linha
        for (int i = 0; i < totalEnemiesToSpawn; i++) {
            // Calcula a posi��o de cada inimigo
            Vector3 spawnPosition = startPosition + (offsetDirection == Vector3.right || offsetDirection == Vector3.left
                ? Vector3.up * (i * spacingBetweenEnemies - offset) // Caso seja Direita ou Esquerda, ajuste no eixo Y
                : Vector3.right * (i * spacingBetweenEnemies - offset)); // Caso seja Acima ou Abaixo, ajuste no eixo X

            // Instancia o inimigo na posi��o calculada
            GameObject enemyInstance = Object.Instantiate(selectedConfig.enemyPrefab, spawnPosition, Quaternion.identity);

            // Inicializa o inimigo com as configura��es do EnemyConfig
            BaseEnemy enemy = enemyInstance.GetComponent<BaseEnemy>();
            if (enemy != null) {
                enemy.Initialize(selectedConfig); // Aplica a configura��o ao inimigo instanciado
            }
        }
    }
}
