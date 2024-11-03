using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : Weapon
{
    protected override void Start() {
        base.Start();
        weaponName = "Arma Padrão";
        damage = 10;
        fireRate = 1f;
        reloadSpeed = 1.0f;
        hasUnlimitedAmmo = true;
    }

    public override void Use() {
        Shoot();
        //Debug.Log("Firing " + weaponName);
    }

    public override void Reload() {
        // Arma padrão tem munição ilimitada, então não precisa recarregar
        Debug.Log($"{weaponName} has unlimited ammo.");
    }
}
