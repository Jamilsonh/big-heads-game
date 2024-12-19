using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Tooltip("ScriptableObject que contém os dados da arma")]
    public WeaponScriptableConfig weaponData;
    public Transform firePoint;   // Ponto de saída do projétil

    private float nextFireTime;
    public int currentAmmo; // Armazena a munição atual da arma

    public MuzzleEffect muzzleEffect; // Referência ao script do efeito

    protected virtual void Start() {
        currentAmmo = weaponData.maxAmmo;
    }

    public abstract void Use();

    public virtual void Reload() {
        Debug.Log($"{weaponData.weaponName} reloading...");
        currentAmmo = weaponData.maxAmmo;
    }

    protected void Shoot() {
        if (currentAmmo > 0 || weaponData.hasUnlimitedAmmo) {
            if (Time.time >= nextFireTime) {
                nextFireTime = Time.time + 1f / weaponData.fireRate;

                // Reduz a munição se a arma não tiver munição ilimitada
                if (!weaponData.hasUnlimitedAmmo) {
                    currentAmmo--;
                }

                // Instancia o projétil no FirePoint da arma
                GameObject projectile = Instantiate(weaponData.projectilePrefab, firePoint.position, Quaternion.identity);
                Bullet projScript = projectile.GetComponent<Bullet>();

                // Define a direção do projétil com base na posição do mouse
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0; // Ajusta a posição Z para 2D
                projScript.SetDirection(mousePosition);

                // Define o dano do projétil com base na arma
                projScript.damage = weaponData.damage;

                // Mostra o efeito de pólvora
                if (muzzleEffect != null) {
                    muzzleEffect.ShowEffect();
                }
            }
        }
        else {
            Debug.Log("Out of ammo!");
        }
    }
}
