using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnManager : MonoBehaviour
{
    public List<MonoBehaviour> spawnStrategies; // Lista de estrat�gias de spawn
    public float strategySwitchInterval = 10f; // Tempo entre troca de estrat�gias

    private MonoBehaviour currentStrategy; // Estrat�gia de spawn atual
    private int currentStrategyIndex = 0;
    private float switchTimer;

    void Start() {
        if (spawnStrategies.Count > 0) {
            // Inicializa com a primeira estrat�gia
            SetSpawnStrategy(0);
        }
    }

    void Update() {
        // Alterna entre estrat�gias automaticamente ap�s o intervalo definido
        switchTimer += Time.deltaTime;
        if (switchTimer >= strategySwitchInterval) {
            CycleSpawnStrategy();
            switchTimer = 0f;
        }
    }

    private void SetSpawnStrategy(int index) {
        if (index >= 0 && index < spawnStrategies.Count) {
            // Desativa a estrat�gia anterior, se houver
            if (currentStrategy != null)
                currentStrategy.enabled = false;

            // Ativa a nova estrat�gia
            currentStrategy = spawnStrategies[index];
            currentStrategy.enabled = true;
            currentStrategyIndex = index;
        }
    }

    private void CycleSpawnStrategy() {
        // Passa para a pr�xima estrat�gia na lista
        int nextIndex = (currentStrategyIndex + 1) % spawnStrategies.Count;
        SetSpawnStrategy(nextIndex);
    }
}
