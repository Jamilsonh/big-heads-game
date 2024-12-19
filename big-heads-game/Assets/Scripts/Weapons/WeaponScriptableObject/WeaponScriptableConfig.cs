using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/WeaponScriptableConfig")]
public class WeaponScriptableConfig : ScriptableObject
{
    public string weaponName;
    public int damage;
    public float fireRate;
    public float reloadSpeed;
    public bool hasUnlimitedAmmo;
    public int maxAmmo;
    public GameObject projectilePrefab;
}
