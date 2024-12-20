using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform weaponHolder;
    public Weapon currentWeapon;
    public bool isShooting;

    void Start() {
        if (currentWeapon != null) {
            InitializeWeapon(currentWeapon);
            Debug.Log($"Starting with weapon: {currentWeapon.weaponData.weaponName}");

            // Atualiza a UI com base na arma inicial
            FindObjectOfType<WeaponUIManager>()?.UpdateUI();
        }
        else {
            Debug.LogWarning("No starting weapon assigned!");
        }
    }

    void Update() {
        if (Input.GetButton("Fire1") && currentWeapon != null) {
            currentWeapon.Use();
            isShooting = true;

            FindObjectOfType<WeaponUIManager>()?.UpdateUI();
        }
        else {
            isShooting = false;
        }
    }

    public void EquipWeapon(WeaponScriptableConfig newWeaponData, GameObject weaponPrefab) {
        if (currentWeapon != null) {
            Destroy(currentWeapon.gameObject);
        }

        GameObject newWeapon = Instantiate(weaponPrefab, weaponHolder.position, Quaternion.identity, weaponHolder);
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localRotation = Quaternion.identity;
        newWeapon.transform.localScale = Vector3.one;

        currentWeapon = newWeapon.GetComponent<Weapon>();
        currentWeapon.weaponData = newWeaponData;

        // Inicializa a munição da nova arma
        InitializeWeapon(currentWeapon);

        // Atualiza a UI com os valores da nova arma
        FindObjectOfType<WeaponUIManager>()?.UpdateUI();

        Debug.Log($"Equipped new weapon: {currentWeapon.weaponData.weaponName}");
    }

    private void InitializeWeapon(Weapon weapon) {
        // Inicializa munição com base nos valores do ScriptableObject
        weapon.currentAmmo = weapon.weaponData.startingAmmo;
        weapon.totalAmmo = weapon.weaponData.startingTotalAmmo;
    }

    // Detecta colisão com armas no chão
    private void OnTriggerEnter2D(Collider2D collision) {
        WeaponPickUp weaponPickup = collision.GetComponent<WeaponPickUp>();
        if (weaponPickup != null) {
            // Equipa a nova arma
            EquipWeapon(weaponPickup.weaponData, weaponPickup.weaponPrefab);

            // Destroi o objeto da arma no chão
            Destroy(collision.gameObject);
        }
    }
}
