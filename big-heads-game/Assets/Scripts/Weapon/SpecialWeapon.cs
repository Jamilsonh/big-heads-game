using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWeapon : Weapon
{
    public bool penetratesEnemy; // Atributo exclusivo

    protected override void Start() {
        base.Start();
        weaponName = "Arma Dourada";
        damage = 25;
        fireRate = 0.5f;
        reloadSpeed = 1.0f;
        hasUnlimitedAmmo = false;
        maxAmmo = 20;
        penetratesEnemy = true;
    }

    public override void Use() {
        if (currentAmmo > 0) {
            Debug.Log("Firing " + weaponName + " with penetrating shot.");
            currentAmmo--;
            Shoot();
        }
        else {
            Debug.Log(weaponName + " is out of ammo!");
            Reload();
        }
    }
}
