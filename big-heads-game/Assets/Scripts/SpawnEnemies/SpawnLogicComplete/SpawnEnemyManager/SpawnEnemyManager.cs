using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyManager : MonoBehaviour
{
    public Transform playerTransform; // Referência à posição do jogador
    public List<EnemyConfig> enemyConfigs; // Lista de configurações de inimigos
    public float strategySwitchInterval = 10f; // Tempo para alternar estratégias de spawn
    public float initialDelay = 5f; // Tempo de atraso antes do primeiro spawn

    private List<ISpawnStrategy> spawnStrategies; // Lista de estratégias de spawn implementadas
    private int currentStrategyIndex = 0; // Índice da estratégia atual
    private float switchTimer; // Timer para controlar a troca de estratégias
    private bool initialDelayPassed = false; // Indica se o atraso inicial foi concluído

    void Start() {
        // Inicializa as estratégias disponíveis de spawn
        spawnStrategies = new List<ISpawnStrategy> {
            //new SpawnEnemyFastSingle(playerTransform, enemyConfigs, "Spawn Fast Single"),
            //new SpawnEnemyUniqueManyEnemies(playerTransform, enemyConfigs, "Spawn Unique Many Enemies"),
            //new SpawnOneSide(playerTransform, enemyConfigs, "Spawn One Side"),
            //new SpawnTwoSides(playerTransform, enemyConfigs, "Spawn Two Sides")
            new SpawnCircle(playerTransform, enemyConfigs, "Spawn Circle")
        };
    }

    void Update() {
        // Gerencia o atraso inicial antes de ativar o spawn
        if (!initialDelayPassed) {
            initialDelay -= Time.deltaTime; // Reduz o tempo do atraso
            if (initialDelay <= 0f) {
                initialDelayPassed = true; // Marca como concluído
                ActivateFirstSpawn(); // Ativa a primeira estratégia
            }
            return; // Aguarda o atraso inicial terminar
        }

        // Alterna entre estratégias após o intervalo definido
        switchTimer += Time.deltaTime; // Incrementa o timer
        if (switchTimer >= strategySwitchInterval) {
            CycleSpawnStrategy(); // Alterna para a próxima estratégia
            switchTimer = 0f; // Reseta o timer
        }

        // Atualiza a lógica da estratégia atual
        spawnStrategies[currentStrategyIndex].Update();
    }

    private void ActivateFirstSpawn() {
        // Ativa a primeira estratégia e registra a ação no console
        spawnStrategies[currentStrategyIndex].Start();
        Debug.Log($"First Spawn Strategy Started: {spawnStrategies[currentStrategyIndex].GetName()}");
    }

    private void CycleSpawnStrategy() {
        // Finaliza a estratégia atual antes de alternar
        spawnStrategies[currentStrategyIndex].End();

        // Passa para a próxima estratégia na lista (em loop)
        currentStrategyIndex = (currentStrategyIndex + 1) % spawnStrategies.Count;

        // Ativa a nova estratégia e registra no console
        spawnStrategies[currentStrategyIndex].Start();
        Debug.Log($"Spawn Strategy Started: {spawnStrategies[currentStrategyIndex].GetName()}");
    }
}
