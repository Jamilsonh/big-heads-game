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

    protected virtual void Start() {
        // Inicializa valores din�micos com base no ScriptableObject
        currentAmmo = weaponData.startingAmmo;
        totalAmmo = weaponData.startingTotalAmmo;
    }

    public abstract void Use();

    public virtual void Reload() {
        if (!weaponData.hasUnlimitedAmmo && totalAmmo > 0) {
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
        }
        else if (weaponData.hasUnlimitedAmmo) {
            currentAmmo = weaponData.maxAmmo;
            Debug.Log($"{weaponData.weaponName} has unlimited ammo. Reloaded to max.");
        }
        else {
            Debug.Log("No ammo left to reload!");
        }

        FindObjectOfType<WeaponUIManager>()?.UpdateUI();
    }

    protected void Shoot() {
        if (currentAmmo > 0 || weaponData.hasUnlimitedAmmo) {
            if (Time.time >= nextFireTime) {
                nextFireTime = Time.time + 1f / weaponData.fireRate;

                if (!weaponData.hasUnlimitedAmmo) {
                    currentAmmo--;
                }

                GameObject projectile = Instantiate(weaponData.projectilePrefab, firePoint.position, Quaternion.identity);
                Bullet projScript = projectile.GetComponent<Bullet>();
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
                projScript.SetDirection(mousePosition);
                projScript.damage = weaponData.damage;

                if (muzzleEffect != null) {
                    muzzleEffect.ShowEffect();
                }

                FindObjectOfType<WeaponUIManager>()?.UpdateUI();
            }
        }
        else {
            Debug.Log("Out of ammo! Reloading...");
            Reload();
        }
    }
}
