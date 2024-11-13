using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Config")]
public class EnemyConfig : ScriptableObject
{
    public string enemyName;
    public GameObject enemyPrefab; // Prefab do inimigo que inclui anima��o
    public float health;
    public float minSpeed; // Velocidade m�nima
    public float maxSpeed; // Velocidade m�xima
}
