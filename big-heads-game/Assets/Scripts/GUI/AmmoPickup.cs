using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [Tooltip("Quantidade de muni��o que este objeto concede")]
    public int ammoAmount = 30;

    [Tooltip("Som emitido ao pegar a muni��o")]
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision) {
        // Verifica se o objeto que colidiu � o player e possui uma arma
        Weapon weapon = collision.GetComponent<Weapon>();
        if (weapon != null) {
            // Incrementa a muni��o total da arma
            weapon.totalAmmo += ammoAmount;

            // Atualiza a UI de muni��o, se necess�rio
            FindObjectOfType<WeaponUIManager>()?.UpdateUI();

            // Toca o som de coleta de muni��o usando o AudioManager
            PlayPickupSound();

            // Opcional: Adicionar um efeito visual ou som de coleta
            Debug.Log("Muni��o coletada!");

            // Destroi o objeto de muni��o 
            Destroy(gameObject);
        }
    }

    private void PlayPickupSound() {
        if (pickupSound != null) {
            AudioManager.Instance?.PlaySFX(pickupSound);
        }
    }
}
