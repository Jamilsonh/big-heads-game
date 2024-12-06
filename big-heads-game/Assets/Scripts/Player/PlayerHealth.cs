using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Para manipular elementos da UI

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthBar; // Refer�ncia ao slider da barra de vida

    private SpriteRenderer spriteRenderer; // Refer�ncia ao SpriteRenderer
    private Color originalColor; // Cor original do player
    public Color flashColor = Color.red; // Cor do flash (pode ajustar para o que desejar)
    public float flashDuration = 0.1f; // Dura��o do flash

    void Start() {
        currentHealth = maxHealth;
        UpdateHealthBar();

        // Obt�m o SpriteRenderer do player
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) {
            originalColor = spriteRenderer.color; // Salva a cor original
        }
        else {
            Debug.LogError("SpriteRenderer n�o encontrado no player.");
        }
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Garante que a vida fique entre 0 e o m�ximo
        UpdateHealthBar();

        if (spriteRenderer != null) {
            StartCoroutine(Flash()); // Inicia o flash ao receber dano
        }

        if (currentHealth <= 0) {
            Die();
        }
    }

    public void Heal(int amount) {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Garante que a vida n�o ultrapasse o m�ximo
        UpdateHealthBar();
    }

    private void UpdateHealthBar() {
        if (healthBar != null) {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }

    private void Die() {
        Debug.Log("Player morreu!");
        // Adicione aqui a l�gica para morte do jogador (ex.: reiniciar o jogo, animar morte, etc.) 
    }

    public void OnHit(int damage) {
        // Aplica o dano ao jogador
        TakeDamage(damage);

        // Mensagem de debug
        Debug.Log($"Jogador foi atingido e sofreu {damage} de dano.");
    }

    private IEnumerator Flash() {
        // Altera a cor do sprite para a cor de flash
        spriteRenderer.color = flashColor;

        // Espera por um curto per�odo de tempo
        yield return new WaitForSeconds(flashDuration);

        // Restaura a cor original do sprite
        spriteRenderer.color = originalColor;
    }
}
