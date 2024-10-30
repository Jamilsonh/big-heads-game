using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    public string weaponName;
    public int damage;
    public float fireRate;
    public float reloadSpeed;
    public bool hasUnlimitedAmmo;
    public int maxAmmo;
    protected int currentAmmo;

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
}
