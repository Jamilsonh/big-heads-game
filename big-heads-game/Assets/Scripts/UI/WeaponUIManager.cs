using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIManager : MonoBehaviour
{
    public Image weaponIcon;          // Miniatura da arma
    public TextMeshProUGUI currentAmmoText;      // Texto para munição atual
    public TextMeshProUGUI totalAmmoText;        // Texto para munição total

    private WeaponManager weaponManager;

    void Start() {
        // Referência ao WeaponManager
        weaponManager = FindObjectOfType<WeaponManager>();
        if (weaponManager == null) {
            Debug.LogError("WeaponManager não encontrado!");
        }

        UpdateUI(); // Atualiza a UI no início
    }

    public void UpdateUI() {
        if (weaponManager.currentWeapon != null) {
            // Atualiza miniatura da arma
            weaponIcon.sprite = weaponManager.currentWeapon.weaponData.icon; // Supondo que o ScriptableObject tenha um ícone

            // Atualiza textos de munição
            currentAmmoText.text = weaponManager.currentWeapon.currentAmmo.ToString();
            totalAmmoText.text = weaponManager.currentWeapon.totalAmmo.ToString(); // Supondo que o ScriptableObject tenha um totalAmmo
        }
        else {
            // Limpa a UI se não houver arma equipada
            weaponIcon.sprite = null;
            currentAmmoText.text = "0";
            totalAmmoText.text = "0";
        }
    }
}
