using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWp : Weapon
{
    public override void Use() {
        Shoot();
    }

    public override void Reload() {
        if (weaponData.hasUnlimitedAmmo) {
            Debug.Log($"{weaponData.weaponName} has unlimited ammo.");
        } else {
            base.Reload();
        }
    }
}
