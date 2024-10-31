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

    public GameObject projectilePrefab; // Prefab do projétil a ser instanciado
    public Transform firePoint; // Ponto de saída do projétil na arma

    private float nextFireTime;

    protected virtual void Start() {
        currentAmmo = maxAmmo;
    }

    // Método para atirar
    public abstract void Use();

    // Método para recarregar
    public virtual void Reload() {
        Debug.Log($"{weaponName} reloading...");
        currentAmmo = maxAmmo;
    }

    // Método de tiro que instância o projétil e define sua direção
    protected void Shoot() {
        if (Time.time >= nextFireTime) {
            nextFireTime = Time.time + 1f / fireRate;

            // Instancia o projétil no FirePoint da arma
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Bullet projScript = projectile.GetComponent<Bullet>();

            // Define a direção do projétil com base na posição do mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Ajusta a posição Z para 2D
            projScript.SetDirection(mousePosition);

            // Define o dano do projétil com base na arma
            projScript.damage = damage;
        }
    }
}
