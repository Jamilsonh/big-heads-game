using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour 
{
    public string weaponName;
    public int damage;
    public float fireRate;
    public float reloadSpeed;
    public bool hasUnlimitedAmmo;
    public int maxAmmo;
    protected int currentAmmo;

    public GameObject projectilePrefab; // Prefab do proj�til a ser instanciado
    public Transform firePoint; // Ponto de sa�da do proj�til na arma

    private float nextFireTime;

    protected virtual void Start() {
        currentAmmo = maxAmmo;
    }

    // M�todo para atirar
    public abstract void Use();

    // M�todo para recarregar
    public virtual void Reload() {
        Debug.Log($"{weaponName} reloading...");
        currentAmmo = maxAmmo;
    }

    // M�todo de tiro que inst�ncia o proj�til e define sua dire��o
    protected void Shoot() {
        if (Time.time >= nextFireTime) {
            nextFireTime = Time.time + 1f / fireRate;

            // Instancia o proj�til no FirePoint da arma
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Bullet projScript = projectile.GetComponent<Bullet>();

            // Define a dire��o do proj�til com base na posi��o do mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Ajusta a posi��o Z para 2D
            projScript.SetDirection(mousePosition);

            // Define o dano do proj�til com base na arma
            projScript.damage = damage;
        }
    }
}
