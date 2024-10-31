using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleWeapon : Weapon
{
    protected override void Start() {
        base.Start();
        weaponName = "Rifle";
        damage = 15;
        fireRate = 0.2f;
        reloadSpeed = 1.5f;
        hasUnlimitedAmmo = false;
        maxAmmo = 30;
    }

    public override void Use() {
        if (currentAmmo > 0) {
            Debug.Log("Firing " + weaponName);
            currentAmmo--;
            Shoot();
        }
        else {
            Debug.Log(weaponName + " is out of ammo!");
            Reload();
        }
    }
}
