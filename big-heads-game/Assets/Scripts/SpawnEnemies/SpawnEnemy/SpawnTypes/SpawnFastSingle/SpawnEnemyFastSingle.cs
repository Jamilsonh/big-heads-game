using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyFastSingle : ISpawnStrategy
{
    // Referência à posição do jogador, usada para calcular o spawn dos inimigos
    private Transform playerTransform;
    // Lista de configurações de inimigos disponíveis para spawn
    private List<EnemyConfig> enemyConfigs;
    // Nome da estratégia, útil para identificação
    private string name;
    // Intervalo entre cada spawn em segundos
    private float spawnInterval = 0.5f;
    // Número mínimo e máximo de inimigos a spawnar em cada iteração
    private int minEnemiesToSpawn = 2;
    private int maxEnemiesToSpawn = 5;
    // Espaçamento entre inimigos na formação
    private float spacingBetweenEnemies = 2f;
    // Distância inicial entre o jogador e o spawn dos inimigos
    private float distanceFromPlayer = 15f;
    // Quantidade total de inimigos a ser spawnada
    private int totalEnemiesToSpawn = 10;

    // Timer para controlar o intervalo de spawn
    private float spawnTimer;
    // Contador de inimigos já spawnados
    private int spawnedEnemiesCount;

    // Construtor para inicializar os parâmetros da estratégia
    public SpawnEnemyFastSingle(Transform playerTransform, List<EnemyConfig> enemyConfigs, string name) {
        this.playerTransform = playerTransform; // Define a posição do jogador
        this.enemyConfigs = enemyConfigs; // Lista de inimigos disponíveis
        this.name = name; // Define o nome da estratégia
    }

    // Método chamado quando a estratégia começa
    public void Start() {
        spawnTimer = 0f; // Reseta o timer
        spawnedEnemiesCount = 0; // Reseta o contador de inimigos spawnados
    }

    // Método chamado a cada frame para atualizar a lógica da estratégia
    public void Update() {
        // Verifica se ainda há inimigos para spawnar
        if (spawnedEnemiesCount < totalEnemiesToSpawn) {
            spawnTimer += Time.deltaTime; // Incrementa o timer com o tempo do frame atual
            if (spawnTimer >= spawnInterval) {
                SpawnEnemiesInSingleDirection(); // Realiza o spawn dos inimigos
                spawnTimer = 0f; // Reseta o timer após o spawn
            }
        }
    }

    // Método chamado ao final da estratégia (implementação futura, se necessário)
    public void End() {
        // Limpeza ou finalização de recursos, se necessário
    }

    // Retorna o nome da estratégia
    public string GetName() {
        return name;
    }

    // Método privado que realiza o spawn de inimigos em uma única direção
    private void SpawnEnemiesInSingleDirection() {
        // Seleciona uma configuração de inimigo aleatória da lista
        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

        // Determina uma direção aleatória: direita, esquerda, acima ou abaixo do jogador
        int direction = Random.Range(0, 4);

        // Calcula o número de inimigos a spawnar, limitado pelo total restante
        int remainingEnemies = totalEnemiesToSpawn - spawnedEnemiesCount;
        int enemiesToSpawn = Mathf.Min(Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn + 1), remainingEnemies);

        // Define a posição inicial de spawn com base na direção escolhida
        Vector3 startPosition = playerTransform.position;
        Vector3 offsetDirection = Vector3.zero; // Vetor que representa a direção do spawn

        // Determina a direção do spawn e ajusta a posição inicial
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

        // Calcula o deslocamento para centralizar o grupo de inimigos
        float offset = (enemiesToSpawn - 1) / 2f * spacingBetweenEnemies;

        // Loop para instanciar os inimigos na formação
        for (int i = 0; i < enemiesToSpawn; i++) {
            // Calcula a posição de cada inimigo
            Vector3 spawnPosition = startPosition + (offsetDirection == Vector3.right || offsetDirection == Vector3.left
                ? Vector3.up * (i * spacingBetweenEnemies - offset) // Direita ou Esquerda -> alinhe no eixo Y
                : Vector3.right * (i * spacingBetweenEnemies - offset)); // Acima ou Abaixo -> alinhe no eixo X

            // Instancia o inimigo na posição calculada
            GameObject enemyInstance = Object.Instantiate(selectedConfig.enemyPrefab, spawnPosition, Quaternion.identity);

            // Inicializa o inimigo com as configurações do EnemyConfig
            BaseEnemy enemy = enemyInstance.GetComponent<BaseEnemy>();
            if (enemy != null) {
                enemy.Initialize(selectedConfig); // Configura o inimigo com os atributos selecionados
            }
        }

        // Atualiza o contador de inimigos já spawnados
        spawnedEnemiesCount += enemiesToSpawn;
    }
}
