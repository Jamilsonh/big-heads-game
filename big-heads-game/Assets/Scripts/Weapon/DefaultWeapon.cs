using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : Weapon
{
    protected override void Start() {
        base.Start();
        weaponName = "Arma Padr�o";
        damage = 10;
        fireRate = 0.5f;
        reloadSpeed = 1.0f;
        hasUnlimitedAmmo = true;
    }

    public override void Use() {
        Debug.Log("Firing " + weaponName);
    }

    public override void Reload() {
        // Arma padr�o tem muni��o ilimitada, ent�o n�o precisa recarregar
        Debug.Log($"{weaponName} has unlimited ammo.");
    }
}
