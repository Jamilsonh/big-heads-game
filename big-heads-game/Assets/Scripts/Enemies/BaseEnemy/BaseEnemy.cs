using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float health;
    public float speed;

    protected Transform player;
    private SpriteRenderer spriteRenderer;

    public virtual void Initialize(EnemyConfig config) {
        health = config.health;
        speed = Random.Range(config.minSpeed, config.maxSpeed);
        player = GameObject.FindWithTag("Player").transform; // Assumindo que o player tem a tag "Player"
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtém o SpriteRenderer do inimigo
    }

    protected virtual void Update() {
        MoveTowardsPlayer();
        FlipSprite();
    }

    protected void MoveTowardsPlayer() {
        if (player != null) {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime; 
        }
    }

    private void FlipSprite() {
        if (player != null) {
            // Se o inimigo está á direita do jogador, vira o sprite para a esquerda, e vice-versa
            if (player.position.x < transform.position.x) {
                spriteRenderer.flipX = true; // Vira o sprite para a esquerda
            }
            else {
                spriteRenderer.flipX = false; // Vira o sprite para a direita
            }
        } 
    }
}
