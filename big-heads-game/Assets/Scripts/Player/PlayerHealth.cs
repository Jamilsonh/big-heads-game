using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Para manipular elementos da UI

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthBar; // Refer�ncia ao slider da barra de vida

    void Start() {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Garante que a vida fique entre 0 e o m�ximo
        UpdateHealthBar();

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
}
