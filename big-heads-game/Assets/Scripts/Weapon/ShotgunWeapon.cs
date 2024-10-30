using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : Weapon
{
    public int pelletCount; // Atributo exclusivo para tiro espalhado

    protected override void Start() {
        base.Start();
        weaponName = "Shotgun";
        damage = 50;
        fireRate = 1.5f;
        reloadSpeed = 2.0f;
        hasUnlimitedAmmo = false;
        maxAmmo = 8;
        pelletCount = 5; // Define o número de projéteis espalhados
    }

    public override void Use() {
        if (currentAmmo > 0) {
            Debug.Log("Firing " + weaponName + " with spread shot of " + pelletCount + " pellets.");
            currentAmmo--;
        }
        else {
            Debug.Log(weaponName + " is out of ammo!");
            Reload();
        }
    }
}
