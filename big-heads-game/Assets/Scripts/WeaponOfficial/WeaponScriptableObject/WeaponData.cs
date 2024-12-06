using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/WeaponData")]
public class WeaponData : ScriptableObject {
    public string weaponName;
    public int damage;
    public float fireRate;
    public float reloadSpeed;
    public bool hasUnlimitedAmmo;
    public int maxAmmo;
    public GameObject projectilePrefab;
    public Sprite weaponThumbnail;
}
