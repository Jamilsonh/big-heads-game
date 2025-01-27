using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Tooltip("Quantidade de vida que este objeto concede")]
    public int healAmount = 20;

    [Tooltip("Som emitido ao pegar a munição")]
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision) {
        // Verifica se o objeto que colidiu possui o componente PlayerHealth
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null) {
            // Cura o jogador
            playerHealth.Heal(healAmount);

            // Opcional: Adicionar um efeito visual ou som de cura
            Debug.Log("Vida recuperada!");

            // Toca o som de coleta de munição usando o AudioManager
            PlayPickupSound();

            // Destroi o objeto de cura
            Destroy(gameObject); 
        }
    }

    private void PlayPickupSound() {
        if (pickupSound != null) {
            AudioManager.Instance?.PlaySFX(pickupSound);
        }
    }
}
