using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {
    private float health;
    private float speed;

    protected Transform player;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private Color originalColor; // Cor original do inimigo
    public Color flashColor = Color.white; // Cor do flash
    public float flashDuration = 0.1f; // Duração do flash

    public GameObject deathEffect; // Referência para o prefab do efeito visual

    public virtual void Initialize(EnemyConfig config) {
        health = config.health;
        speed = Random.Range(config.minSpeed, config.maxSpeed);
        player = GameObject.FindWithTag("Player").transform; // Assumindo que o player tem a tag "Player"
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtém o SpriteRenderer do inimigo
        rb = GetComponent<Rigidbody2D>(); // Obtém o Rigidbody2D do inimigo

        originalColor = spriteRenderer.color;

        // Certifique-se de que o Rigidbody2D existe
        if (rb == null) {
            Debug.LogError("Rigidbody2D não encontrado no inimigo.");
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;
        Debug.Log($"{gameObject.name} recebeu {damage} de dano. Vida restante: {health}");

        if (health <= 0) {
            Die();
        } else {
            StartCoroutine(Flash());
        }
    }

    private IEnumerator Flash() {
        // Altera a cor do sprite para a cor de flash
        spriteRenderer.color = flashColor;

        // Espera por um curto período de tempo
        yield return new WaitForSeconds(flashDuration);

        // Restaura a cor original do sprite
        spriteRenderer.color = originalColor;
    }

    private void Die() {
        Debug.Log($"{gameObject.name} foi destruído.");

        // Instancia o efeito visual no local do inimigo
        if (deathEffect != null) {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        else {
            Debug.LogWarning("Efeito de morte não atribuído ao inimigo.");
        }

        // Destroi o inimigo
        Destroy(gameObject);
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
