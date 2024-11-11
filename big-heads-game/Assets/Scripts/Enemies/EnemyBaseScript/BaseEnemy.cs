using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float health;
    public float speed;

    protected Transform player;

    public virtual void Initialize(EnemyConfig config) {
        health = config.health;
        speed = config.speed;
        player = GameObject.FindWithTag("Player").transform; // Assumindo que o player tem a tag "Player"
    }

    protected virtual void Update() {
        MoveTowardsPlayer();
    }

    protected void MoveTowardsPlayer() {
        if (player != null) {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime; 
        }
    }
}
