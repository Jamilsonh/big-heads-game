using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyFastSingle : ISpawnStrategy
{
    // Refer�ncia � posi��o do jogador, usada para calcular o spawn dos inimigos
    private Transform playerTransform;
    // Lista de configura��es de inimigos dispon�veis para spawn
    private List<EnemyConfig> enemyConfigs;
    // Nome da estrat�gia, �til para identifica��o
    private string name;
    // Intervalo entre cada spawn em segundos
    private float spawnInterval = 0.5f;
    // N�mero m�nimo e m�ximo de inimigos a spawnar em cada itera��o
    private int minEnemiesToSpawn = 2;
    private int maxEnemiesToSpawn = 5;
    // Espa�amento entre inimigos na forma��o
    private float spacingBetweenEnemies = 2f;
    // Dist�ncia inicial entre o jogador e o spawn dos inimigos
    private float distanceFromPlayer = 15f;
    // Quantidade total de inimigos a ser spawnada
    private int totalEnemiesToSpawn = 10;

    // Timer para controlar o intervalo de spawn
    private float spawnTimer;
    // Contador de inimigos j� spawnados
    private int spawnedEnemiesCount;

    // Construtor para inicializar os par�metros da estrat�gia
    public SpawnEnemyFastSingle(Transform playerTransform, List<EnemyConfig> enemyConfigs, string name) {
        this.playerTransform = playerTransform; // Define a posi��o do jogador
        this.enemyConfigs = enemyConfigs; // Lista de inimigos dispon�veis
        this.name = name; // Define o nome da estrat�gia
    }

    // M�todo chamado quando a estrat�gia come�a
    public void Start() {
        spawnTimer = 0f; // Reseta o timer
        spawnedEnemiesCount = 0; // Reseta o contador de inimigos spawnados
    }

    // M�todo chamado a cada frame para atualizar a l�gica da estrat�gia
    public void Update() {
        // Verifica se ainda h� inimigos para spawnar
        if (spawnedEnemiesCount < totalEnemiesToSpawn) {
            spawnTimer += Time.deltaTime; // Incrementa o timer com o tempo do frame atual
            if (spawnTimer >= spawnInterval) {
                SpawnEnemiesInSingleDirection(); // Realiza o spawn dos inimigos
                spawnTimer = 0f; // Reseta o timer ap�s o spawn
            }
        }
    }

    // M�todo chamado ao final da estrat�gia (implementa��o futura, se necess�rio)
    public void End() {
        // Limpeza ou finaliza��o de recursos, se necess�rio
    }

    // Retorna o nome da estrat�gia
    public string GetName() {
        return name;
    }

    // M�todo privado que realiza o spawn de inimigos em uma �nica dire��o
    private void SpawnEnemiesInSingleDirection() {
        // Seleciona uma configura��o de inimigo aleat�ria da lista
        EnemyConfig selectedConfig = enemyConfigs[Random.Range(0, enemyConfigs.Count)];

        // Determina uma dire��o aleat�ria: direita, esquerda, acima ou abaixo do jogador
        int direction = Random.Range(0, 4);

        // Calcula o n�mero de inimigos a spawnar, limitado pelo total restante
        int remainingEnemies = totalEnemiesToSpawn - spawnedEnemiesCount;
        int enemiesToSpawn = Mathf.Min(Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn + 1), remainingEnemies);

        // Define a posi��o inicial de spawn com base na dire��o escolhida
        Vector3 startPosition = playerTransform.position;
        Vector3 offsetDirection = Vector3.zero; // Vetor que representa a dire��o do spawn

        // Determina a dire��o do spawn e ajusta a posi��o inicial
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

        // Loop para instanciar os inimigos na forma��o
        for (int i = 0; i < enemiesToSpawn; i++) {
            // Calcula a posi��o de cada inimigo
            Vector3 spawnPosition = startPosition + (offsetDirection == Vector3.right || offsetDirection == Vector3.left
                ? Vector3.up * (i * spacingBetweenEnemies - offset) // Direita ou Esquerda -> alinhe no eixo Y
                : Vector3.right * (i * spacingBetweenEnemies - offset)); // Acima ou Abaixo -> alinhe no eixo X

            // Instancia o inimigo na posi��o calculada
            GameObject enemyInstance = Object.Instantiate(selectedConfig.enemyPrefab, spawnPosition, Quaternion.identity);

            // Inicializa o inimigo com as configura��es do EnemyConfig
            BaseEnemy enemy = enemyInstance.GetComponent<BaseEnemy>();
            if (enemy != null) {
                enemy.Initialize(selectedConfig); // Configura o inimigo com os atributos selecionados
            }
        }

        // Atualiza o contador de inimigos j� spawnados
        spawnedEnemiesCount += enemiesToSpawn;
    }
}
