using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spawn/SpawnConfig")]
public class SpawnConfigObjects : ScriptableObject
{
    public string spawnType; // Nome do tipo de spawn (ex.: "UniqueMany", "FastSingle")
    public int totalEnemiesToSpawn; // Número total de inimigos no bloco
    public float spawnInterval; // Intervalo entre os spawns
    public float blockDuration; // Duração total do bloco em segundos
    public float distanceFromPlayer; // Distância inicial dos inimigos em relação ao player
    public float spacingBetweenEnemies; // Espaçamento entre inimigos na linha
}
