using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [Tooltip("Quantidade de munição que este objeto concede")]
    public int ammoAmount = 30;

    [Tooltip("Som emitido ao pegar a munição")]
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision) {
        // Verifica se o objeto que colidiu é o player e possui uma arma
        Weapon weapon = collision.GetComponent<Weapon>();
        if (weapon != null) {
            // Incrementa a munição total da arma
            weapon.totalAmmo += ammoAmount;

            // Atualiza a UI de munição, se necessário
            FindObjectOfType<WeaponUIManager>()?.UpdateUI();

            // Toca o som de coleta de munição usando o AudioManager
            PlayPickupSound();

            // Opcional: Adicionar um efeito visual ou som de coleta
            Debug.Log("Munição coletada!");

            // Destroi o objeto de munição 
            Destroy(gameObject);
        }
    }

    private void PlayPickupSound() {
        if (pickupSound != null) {
            AudioManager.Instance?.PlaySFX(pickupSound);
        }
    }
}
