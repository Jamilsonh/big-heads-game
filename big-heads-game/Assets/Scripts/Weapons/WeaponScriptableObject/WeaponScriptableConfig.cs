using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/WeaponScriptableConfig")]
public class WeaponScriptableConfig : ScriptableObject
{
    public string weaponName;
    public Sprite icon;
    public int damage;
    public float fireRate;
    public float reloadSpeed;
    public bool hasUnlimitedAmmo;

    public int maxAmmo;              // Capacidade máxima do carregador
    public int startingAmmo;         // Quantidade inicial de balas no carregador
    public int startingTotalAmmo;    // Quantidade inicial de balas totais

    public GameObject projectilePrefab;
}
