using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyUniqueManyEnemies : ISpawnStrategy
{
    private Transform playerTransform; // Referência à posição do jogador, usada como base para calcular o spawn dos inimigos
    private List<EnemyConfig> enemyConfigs; // Lista de configurações de inimigos que podem ser spawnados
    private string name; // Nome da estratégia de spawn, útil para identificação
    private float spacingBetweenEnemies = 2f; // Espaçamento entre inimigos ao spawnar em linha  
    private float distanceFromPlayer = 15f; // Distância inicial entre o jogador e o local onde os inimigos vão spawnar  
    private int totalEnemiesToSpawn = 10; // Quantidade total de inimigos a ser spawnada
    private bool hasSpawned; // Flag para verificar se os inimigos já foram spawnados

    // Construtor para inicializar os parâmetros da estratégia
    public SpawnEnemyUniqueManyEnemies(Transform playerTransform, List<EnemyConfig> enemyConfigs, string name) {
        this.playerTransform = playerTransform; // Define a posição do jogador
        this.enemyConfigs = enemyConfigs; // Lista de inimigos disponíveis
        this.name = name; // Define o nome da estratégia
    }

    // Método chamado quando a estratégia é iniciada
    public void Start() {
        hasSpawned = false; // Reseta a flag de spawn para falso
    }

    // Método chamado a cada frame para atualizar a estratégia
    public void Update() {
        if (!hasSpawned) {
            SpawnEnemiesInSingleDirection(); // Realiza o spawn dos inimigos em uma direção
            hasSpawned = true; // Marca como concluído para evitar respawns repetidos
        }
    }

    // Método chamado quando a estratégia termina (implementação futura, se necessário)
    public void End() {
        // Limpeza ou finalização de recursos, se necessário
    }

    // Retorna o nome da estratégia
    public string GetName() {
        return name;
    }

    // Método privado que gerencia o spawn dos inimigos em uma única direção
    private void SpawnEnemiesInSingleDirection() {
        // Seleciona uma configuração de inimigo aleatória da lista
        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

        // Determina uma direção aleatória: direita, esquerda, acima ou abaixo do jogador
        int direction = Random.Range(0, 4);

        // Calcula a posição inicial de spawn com base na direção selecionada
        Vector3 startPosition = playerTransform.position;
        Vector3 offsetDirection = Vector3.zero; // Vetor que representa a direção do spawn

        // Determina a direção e ajusta a posição inicial
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
            // Calcula a posição de cada inimigo
            Vector3 spawnPosition = startPosition + (offsetDirection == Vector3.right || offsetDirection == Vector3.left
                ? Vector3.up * (i * spacingBetweenEnemies - offset) // Caso seja Direita ou Esquerda, ajuste no eixo Y
                : Vector3.right * (i * spacingBetweenEnemies - offset)); // Caso seja Acima ou Abaixo, ajuste no eixo X

            // Instancia o inimigo na posição calculada
            GameObject enemyInstance = Object.Instantiate(selectedConfig.enemyPrefab, spawnPosition, Quaternion.identity);

            // Inicializa o inimigo com as configurações do EnemyConfig
            BaseEnemy enemy = enemyInstance.GetComponent<BaseEnemy>();
            if (enemy != null) {
                enemy.Initialize(selectedConfig); // Aplica a configuração ao inimigo instanciado
            }
        }
    }
}
