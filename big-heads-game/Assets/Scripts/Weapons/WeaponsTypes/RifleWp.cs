using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleWp : Weapon
{
    public override void Use() {
        if (this != FindObjectOfType<WeaponManager>().currentWeapon) return; // Apenas a arma equipada pode atirar
        Shoot();
    }

    
}



