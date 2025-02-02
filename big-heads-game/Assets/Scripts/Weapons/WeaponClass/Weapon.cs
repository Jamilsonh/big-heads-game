using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Tooltip("ScriptableObject que contém os dados da arma")]
    public WeaponScriptableConfig weaponData;
    public Transform firePoint;

    //public WeaponUIManager weaponUIManager; // Referência específica da UI

    private float nextFireTime;

    [Header("Munição")]
    public int currentAmmo;   // Munição atual no carregador
    public int totalAmmo;     // Munição total disponível

    public MuzzleEffect muzzleEffect;

    [Header("Player Reload Animation")]
    //public ReloadAnimationManager playerReloadAnimationManager; // Referência ao ReloadAnimationManager do Player

    [Header("Sons de Disparo")]
    public AudioClip[] shootSounds; // Array de sons de disparo

    public bool isReloading = false; // Variável para rastrear o estado de recarregamento

    protected virtual void Start() {
        // Inicializa valores dinâmicos com base no ScriptableObject
        currentAmmo = weaponData.startingAmmo;
        totalAmmo = weaponData.startingTotalAmmo;
    }

    private void Update() {
        
    }

    public abstract void Use();

    /*
    public virtual void Reload() {
        if (isReloading || totalAmmo <= 0 || currentAmmo == weaponData.maxAmmo) {
            return;
        }

        isReloading = true;

        // Toca som de recarregamento
        PlayReloadSound();

        // Reproduz a animação de recarregamento
        if (playerReloadAnimationManager != null) {
            playerReloadAnimationManager.TriggerReloadAnimation(weaponData.reloadAnimationPrefab);
        }

        // Inicia o Coroutine de recarregamento
        StartCoroutine(ReloadCoroutine());
    }*/

    public void RefillAmmo() {
        int ammoNeeded = weaponData.maxAmmo - currentAmmo;
        if (totalAmmo >= ammoNeeded) {
            currentAmmo += ammoNeeded;
            totalAmmo -= ammoNeeded;
        }
        else {
            currentAmmo += totalAmmo;
            totalAmmo = 0;
        }
    }

    public virtual IEnumerator ReloadCoroutine() {
        Debug.Log("Recarregando...");

        yield return new WaitForSeconds(weaponData.reloadSpeed); // Espera pelo tempo de recarga

        int ammoNeeded = weaponData.maxAmmo - currentAmmo;

        if (totalAmmo >= ammoNeeded) {
            currentAmmo += ammoNeeded;
            totalAmmo -= ammoNeeded;
        }
        else {
            currentAmmo += totalAmmo;
            totalAmmo = 0;
        }

        Debug.Log($"{weaponData.weaponName} reloaded. Ammo: {currentAmmo}/{totalAmmo}");

        

        isReloading = false;
    }


    private IEnumerator FinishReload() {
        yield return new WaitForSeconds(weaponData.reloadSpeed); // Tempo de recarregamento
        isReloading = false; // Marca como não recarregando
        Debug.Log("Finished reloading.");

        FindObjectOfType<WeaponUIManager>()?.UpdateUI();
    }

    protected void Shoot() {
        if (isReloading) {
            Debug.Log("Cannot shoot while reloading!");
            return;
        }

        if (currentAmmo > 0 || weaponData.hasUnlimitedAmmo) {
            if (Time.time >= nextFireTime) {
                nextFireTime = Time.time + 1f / weaponData.fireRate;

                if (!weaponData.hasUnlimitedAmmo) {
                    currentAmmo--;
                }

                // Instancia o projétil
                GameObject projectile = Instantiate(weaponData.projectilePrefab, firePoint.position, Quaternion.identity);
                Bullet projScript = projectile.GetComponent<Bullet>();
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
                projScript.SetDirection(mousePosition);
                projScript.damage = weaponData.damage;

                // Mostra o efeito de disparo, se houver
                if (muzzleEffect != null) {
                    muzzleEffect.ShowEffect();
                }

                // Reproduz o som de disparo aleatório
                PlayRandomShootSound();

                FindObjectOfType<WeaponUIManager>()?.UpdateUI();
            }
        }
        else {
            Debug.Log("Out of ammo! Reloading...");
            //Reload();
        }
    }



    private void PlayRandomShootSound() {
        if (shootSounds.Length > 0) {
            int randomIndex = Random.Range(0, shootSounds.Length);
            AudioManager.Instance.PlaySFX(shootSounds[randomIndex]); // Acessa o AudioManager diretamente
        }
        else {
            Debug.LogWarning("Shoot sounds not assigned!");
        }
    }

    public void PlayReloadSound() {
        if (weaponData.reloadSound != null) {
            AudioManager.Instance.PlaySFX(weaponData.reloadSound);
        }
        else {
            Debug.LogWarning("No reload sound assigned!");
        }
    }
}
