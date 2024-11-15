using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spawn/SpawnConfig")]
public class SpawnConfigObjects : ScriptableObject
{
    public string spawnType; // Nome do tipo de spawn (ex.: "UniqueMany", "FastSingle")
    public int totalEnemiesToSpawn; // N�mero total de inimigos no bloco
    public float spawnInterval; // Intervalo entre os spawns
    public float blockDuration; // Dura��o total do bloco em segundos
    public float distanceFromPlayer; // Dist�ncia inicial dos inimigos em rela��o ao player
    public float spacingBetweenEnemies; // Espa�amento entre inimigos na linha
}
