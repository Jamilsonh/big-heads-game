using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform weaponHolder; // Objeto onde a nova arma será equipada
    public WeaponClassNew currentWeapon;  // Referência à arma equipada atualmente
    public bool isShooting;

    void Update() {
        if (Input.GetButton("Fire1") && currentWeapon != null) {
            currentWeapon.Use();
            isShooting = true;
        }
        else {
            isShooting = false; 
        }
    }

    void Start() {
        if (currentWeapon != null) {
            Debug.Log($"Starting with weapon: {currentWeapon.weaponData.weaponName}");
        }
        else {
            Debug.LogWarning("No starting weapon assigned!");
        }
    }

    // Método para equipar uma arma ao pegar no chão
    public void EquipWeapon(WeaponData newWeaponData, GameObject weaponPrefab) {
        // Se já existir uma arma equipada, destrói ela
        if (currentWeapon != null) {
            Destroy(currentWeapon.gameObject);
        }

        // Instancia o novo prefab da arma no holder
        GameObject newWeapon = Instantiate(weaponPrefab, weaponHolder.position, Quaternion.identity, weaponHolder);
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localRotation = Quaternion.identity;
        newWeapon.transform.localScale = Vector3.one;

        // Configura o ScriptableObject como os dados da arma atual
        currentWeapon = newWeapon.GetComponent<WeaponClassNew>();
        currentWeapon.weaponData = newWeaponData;
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
