using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {
    public float health;
    public float speed;

    protected Transform player;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    public virtual void Initialize(EnemyConfig config) {
        health = config.health;
        speed = Random.Range(config.minSpeed, config.maxSpeed);
        player = GameObject.FindWithTag("Player").transform; // Assumindo que o player tem a tag "Player"
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtém o SpriteRenderer do inimigo
        rb = GetComponent<Rigidbody2D>(); // Obtém o Rigidbody2D do inimigo

        // Certifique-se de que o Rigidbody2D existe
        if (rb == null) {
            Debug.LogError("Rigidbody2D não encontrado no inimigo.");
        }
    }

    protected virtual void FixedUpdate() {
        MoveTowardsPlayer();
        FlipSprite();
    }

    protected void MoveTowardsPlayer() {
        if (player != null && rb != null) {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed; // Define a velocidade do Rigidbody2D
        }
    }

    private void FlipSprite() {
        if (player != null) {
            // Se o inimigo está à direita do jogador, vira o sprite para a esquerda, e vice-versa
            spriteRenderer.flipX = player.position.x < transform.position.x;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            // Lógica para dano ou outro efeito ao colidir com o player
            Debug.Log("Inimigo colidiu com o jogador!");
        }
    }
}
