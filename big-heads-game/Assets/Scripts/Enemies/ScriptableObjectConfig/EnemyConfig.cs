using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Config")]
public class EnemyConfig : ScriptableObject
{
    public string enemyName;
    public GameObject enemyPrefab; // Prefab do inimigo que inclui animação
    public float health;
    public float minSpeed; // Velocidade mínima
    public float maxSpeed; // Velocidade máxima
}
