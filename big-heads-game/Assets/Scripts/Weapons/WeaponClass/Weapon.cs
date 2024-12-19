using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Tooltip("ScriptableObject que cont�m os dados da arma")]
    public WeaponScriptableConfig weaponData;
    public Transform firePoint;   // Ponto de sa�da do proj�til

    private float nextFireTime;
    public int currentAmmo; // Armazena a muni��o atual da arma

    public MuzzleEffect muzzleEffect; // Refer�ncia ao script do efeito

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

                // Reduz a muni��o se a arma n�o tiver muni��o ilimitada
                if (!weaponData.hasUnlimitedAmmo) {
                    currentAmmo--;
                }

                // Instancia o proj�til no FirePoint da arma
                GameObject projectile = Instantiate(weaponData.projectilePrefab, firePoint.position, Quaternion.identity);
                Bullet projScript = projectile.GetComponent<Bullet>();

                // Define a dire��o do proj�til com base na posi��o do mouse
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0; // Ajusta a posi��o Z para 2D
                projScript.SetDirection(mousePosition);

                // Define o dano do proj�til com base na arma
                projScript.damage = weaponData.damage;

                // Mostra o efeito de p�lvora
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
