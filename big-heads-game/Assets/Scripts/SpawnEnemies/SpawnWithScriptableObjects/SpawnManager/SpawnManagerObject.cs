using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerObject : MonoBehaviour
{
    public List<SpawnConfigObjects> spawnConfigs; // Lista de configurações no editor
    public List<EnemyConfig> enemyConfigs;
    public Transform playerTransform;

    private ISpawnTypeObject currentSpawn;
    private int currentConfigIndex = 0;
    private float blockTimer = 0f;

    private void Start() {
        LoadNextSpawnConfig();
    }

    private void Update() {
        if (currentSpawn == null) return;

        blockTimer += Time.deltaTime;

        // Se o bloco terminou ou todos os inimigos foram spawnados
        if (blockTimer >= spawnConfigs[currentConfigIndex].blockDuration || currentSpawn.HasFinishedSpawning) {
            blockTimer = 0f;
            LoadNextSpawnConfig();
        }
        else {
            currentSpawn.SpawnEnemies();
        }
    }

    private void LoadNextSpawnConfig() {
        if (currentConfigIndex >= spawnConfigs.Count) {
            Debug.Log("Todos os blocos foram completados!");
            return;
        }

        var config = spawnConfigs[currentConfigIndex];
        currentConfigIndex++;

        // Cria o tipo de spawn com base na configuração
        switch (config.spawnType) {
            case "UniqueMany":
                currentSpawn = GetComponent<SpawnUniqueManyEnemisObject>();
                break;
            case "FastSingle":
                currentSpawn = GetComponent<SpawnFastSingleObject>();
                break;
            default:
                Debug.LogError($"Tipo de spawn desconhecido: {config.spawnType}");
                return;
        }

        currentSpawn.Initialize(config, playerTransform, enemyConfigs);
    }
}
