using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyManager : MonoBehaviour
{
    public Transform playerTransform; // Refer�ncia � posi��o do jogador
    public List<EnemyConfig> enemyConfigs; // Lista de configura��es de inimigos
    public float strategySwitchInterval = 10f; // Tempo para alternar estrat�gias de spawn
    public float initialDelay = 5f; // Tempo de atraso antes do primeiro spawn

    private List<ISpawnStrategy> spawnStrategies; // Lista de estrat�gias de spawn implementadas
    private int currentStrategyIndex = 0; // �ndice da estrat�gia atual
    private float switchTimer; // Timer para controlar a troca de estrat�gias
    private bool initialDelayPassed = false; // Indica se o atraso inicial foi conclu�do

    void Start() {
        // Inicializa as estrat�gias dispon�veis de spawn
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
                initialDelayPassed = true; // Marca como conclu�do
                ActivateFirstSpawn(); // Ativa a primeira estrat�gia
            }
            return; // Aguarda o atraso inicial terminar
        }

        // Alterna entre estrat�gias ap�s o intervalo definido
        switchTimer += Time.deltaTime; // Incrementa o timer
        if (switchTimer >= strategySwitchInterval) {
            CycleSpawnStrategy(); // Alterna para a pr�xima estrat�gia
            switchTimer = 0f; // Reseta o timer
        }

        // Atualiza a l�gica da estrat�gia atual
        spawnStrategies[currentStrategyIndex].Update();
    }

    private void ActivateFirstSpawn() {
        // Ativa a primeira estrat�gia e registra a a��o no console
        spawnStrategies[currentStrategyIndex].Start();
        Debug.Log($"First Spawn Strategy Started: {spawnStrategies[currentStrategyIndex].GetName()}");
    }

    private void CycleSpawnStrategy() {
        // Finaliza a estrat�gia atual antes de alternar
        spawnStrategies[currentStrategyIndex].End();

        // Passa para a pr�xima estrat�gia na lista (em loop)
        currentStrategyIndex = (currentStrategyIndex + 1) % spawnStrategies.Count;

        // Ativa a nova estrat�gia e registra no console
        spawnStrategies[currentStrategyIndex].Start();
        Debug.Log($"Spawn Strategy Started: {spawnStrategies[currentStrategyIndex].GetName()}");
    }
}
