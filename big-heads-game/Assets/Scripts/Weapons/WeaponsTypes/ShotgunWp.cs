using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWp : Weapon
{
    private Coroutine reloadCoroutine; // Refer�ncia ao Coroutine de recarregamento

    public override void Use() {
        // Cancela o recarregamento se atirar
        if (isReloading) {
            StopReloading();
        }

        // Chama a fun��o de disparo m�ltiplo
        ShootShotgun();
    }

    public override void Reload() {
        // Evitar reiniciar o recarregamento enquanto j� est� recarregando
        if (isReloading || currentAmmo == weaponData.maxAmmo || totalAmmo <= 0) return;

        isReloading = true;

        // Toca a anima��o inicial de recarregamento
        if (playerReloadAnimationManager != null) {
            playerReloadAnimationManager.TriggerReloadAnimationShotgun(weaponData.reloadAnimationPrefab);
        }
        else {
            Debug.LogWarning("Player ReloadAnimationManager n�o est� atribu�do!");
        }

        // Inicia o Coroutine de recarregamento bala por bala
        reloadCoroutine = StartCoroutine(ReloadOneByOne());
    }

    private IEnumerator ReloadOneByOne() {
        Debug.Log("Recarregando bala por bala...");

        while (currentAmmo < weaponData.maxAmmo && totalAmmo > 0) {
            // Reproduz som de recarregamento antes de adicionar a bala
            PlayReloadSound();


            // Reproduz anima��o de reload a cada bala recarregada
            if (playerReloadAnimationManager != null) {
                playerReloadAnimationManager.TriggerReloadAnimationShotgun(weaponData.reloadAnimationPrefab);
            }

            // Aguarda a dura��o da anima��o de reload da shotgun
            yield return new WaitForSeconds(playerReloadAnimationManager.reloadAnimationDurationShotgun);        

            currentAmmo++;
            totalAmmo--;

            Debug.Log($"Bala recarregada. Muni��o atual: {currentAmmo}/{totalAmmo}");

            // Atualiza UI
            FindObjectOfType<WeaponUIManager>()?.UpdateUI();

            // Adiciona um atraso extra entre as anima��es
           yield return new WaitForSeconds(0.05f);

            // Se a recarga for interrompida, pare imediatamente
            if (!isReloading) yield break;
        }

        // Finaliza o recarregamento
        Debug.Log("Recarregamento completo!");
        isReloading = false;
        reloadCoroutine = null;
    }

    private void StopReloading() {
        // Interrompe o recarregamento caso esteja em andamento
        if (reloadCoroutine != null) {
            StopCoroutine(reloadCoroutine);
            Debug.Log("Recarregamento interrompido!");
        }

        isReloading = false;
        reloadCoroutine = null;
    }
}
