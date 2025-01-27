using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Tooltip("ScriptableObject que cont�m os dados da arma")]
    public WeaponScriptableConfig weaponData;
    public Transform firePoint;

    private float nextFireTime;

    [Header("Muni��o")]
    public int currentAmmo;   // Muni��o atual no carregador
    public int totalAmmo;     // Muni��o total dispon�vel

    public MuzzleEffect muzzleEffect;

    [Header("Player Reload Animation")]
    public ReloadAnimationManager playerReloadAnimationManager; // Refer�ncia ao ReloadAnimationManager do Player

    [Header("Sons de Disparo")]
    public AudioClip[] shootSounds; // Array de sons de disparo

    public bool isReloading = false; // Vari�vel para rastrear o estado de recarregamento

    protected virtual void Start() {
        // Inicializa valores din�micos com base no ScriptableObject
        currentAmmo = weaponData.startingAmmo;
        totalAmmo = weaponData.startingTotalAmmo;
    }

    private void Update() {
        // Verifica se o bot�o de recarregar foi pressionado
        if (Input.GetKeyDown(KeyCode.R)) {
            Reload();
        }
    }

    public abstract void Use();

    public virtual void Reload() {
        if (isReloading || totalAmmo <= 0 || currentAmmo == weaponData.maxAmmo) {
            return;
        }

        isReloading = true;

        // Toca som de recarregamento
        PlayReloadSound();

        // Reproduz a anima��o de recarregamento
        if (playerReloadAnimationManager != null) {
            playerReloadAnimationManager.TriggerReloadAnimation(weaponData.reloadAnimationPrefab);
        }

        // Inicia o Coroutine de recarregamento
        StartCoroutine(ReloadCoroutine());
    }

    public virtual IEnumerator ReloadCoroutine() {
        // L�gica gen�rica de recarregamento (uma vez s� para todas as balas)
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

        // Atualiza UI e estado
        FindObjectOfType<WeaponUIManager>()?.UpdateUI();
        isReloading = false;
    }


    private IEnumerator FinishReload() {
        yield return new WaitForSeconds(weaponData.reloadSpeed); // Tempo de recarregamento
        isReloading = false; // Marca como n�o recarregando
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

                // Instancia o proj�til
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

                // Reproduz o som de disparo aleat�rio
                PlayRandomShootSound();

                FindObjectOfType<WeaponUIManager>()?.UpdateUI();
            }
        }
        else {
            Debug.Log("Out of ammo! Reloading...");
            Reload();
        }
    }

    protected void ShootShotgun() {
        if (isReloading) {
            Debug.Log("Cannot shoot while reloading!");
            return;
        }

        if (currentAmmo > 0 || weaponData.hasUnlimitedAmmo) {
            if (Time.time >= nextFireTime) {
                nextFireTime = Time.time + 1f / weaponData.fireRate;

                if (!weaponData.hasUnlimitedAmmo) {
                    currentAmmo--; // Subtrair apenas uma muni��o, mesmo com 3 tiros
                }

                // Define os �ngulos dos proj�teis
                float[] angles = { -7f, 0f, 7f }; // Tr�s �ngulos diferentes para os proj�teis

                foreach (float angle in angles) {
                    // Instancia o proj�til
                    GameObject projectile = Instantiate(weaponData.projectilePrefab, firePoint.position, Quaternion.identity);

                    // Configura a dire��o do proj�til
                    Bullet projScript = projectile.GetComponent<Bullet>();
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePosition.z = 0;

                    // Calcula a dire��o com base no �ngulo
                    Vector3 direction = (mousePosition - firePoint.position).normalized;
                    direction = Quaternion.Euler(0, 0, angle) * direction; // Aplica a rota��o do �ngulo
                    projScript.SetDirection(firePoint.position + direction);
                    projScript.damage = weaponData.damage;
                }

                // Mostra o efeito de disparo, se houver
                if (muzzleEffect != null) {
                    muzzleEffect.ShowEffect();
                }

                // Reproduz o som de disparo aleat�rio
                PlayRandomShootSound();

                FindObjectOfType<WeaponUIManager>()?.UpdateUI();
            }
        }
        else {
            Debug.Log("Out of ammo! Reloading...");
            Reload();
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
