using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform weaponHolder;
    public Weapon currentWeapon;
    public bool isShooting;

    // Referência ao ReloadAnimationManager do Player
    private ReloadAnimationManager reloadAnimationManager;
    private WeaponUIManager weaponUIManager; // Adicionado para evitar FindObjectOfType
    public bool isReloading = false; // Variável para rastrear o estado de recarregamento


    void Start() {
        reloadAnimationManager = GetComponentInChildren<ReloadAnimationManager>();
        if (reloadAnimationManager == null) {
            Debug.LogWarning("ReloadAnimationManager not found on the Player!");
        }

        weaponUIManager = FindObjectOfType<WeaponUIManager>(); // Guarda referência da UI

        if (currentWeapon != null) {
            InitializeWeapon(currentWeapon);
            Debug.Log($"Starting with weapon: {currentWeapon.weaponData.weaponName}");

            weaponUIManager?.UpdateUI(); // Atualiza UI corretamente
        }
        else {
            Debug.LogWarning("No starting weapon assigned!");
        }
    }

    void Update() {
        if (Input.GetButton("Fire1") && currentWeapon != null) {
            currentWeapon.Use();
            isShooting = true;
            weaponUIManager?.UpdateUI();
        }
        else {
            isShooting = false;
        }

        // Agora, apenas a arma equipada pode recarregar
        if (Input.GetKeyDown(KeyCode.R) && currentWeapon != null) {
            StartCoroutine(ReloadWeapon());
        }
    }

    private IEnumerator ReloadWeapon() {
        if (isReloading || currentWeapon.totalAmmo <= 0 || currentWeapon.currentAmmo == currentWeapon.weaponData.maxAmmo) {
            yield break; // Evita recarga desnecessária
        }

        isReloading = true;

        // Toca som e animação de recarga
        reloadAnimationManager.TriggerReloadAnimation(currentWeapon.weaponData.reloadAnimationPrefab);
        currentWeapon.PlayReloadSound();

        yield return new WaitForSeconds(currentWeapon.weaponData.reloadSpeed); // Tempo de recarga

        currentWeapon.RefillAmmo(); // Agora apenas adicionamos munição à arma equipada

        weaponUIManager?.UpdateUI(); // Atualiza a UI corretamente
        isReloading = false;
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
        //currentWeapon.weaponUIManager = weaponUIManager; // Passa a UI para a arma equipada

        /*if (reloadAnimationManager != null) {
            currentWeapon.playerReloadAnimationManager = reloadAnimationManager;
        }*/

        InitializeWeapon(currentWeapon);

        weaponUIManager?.UpdateUI();

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
            Debug.Log($"[WeaponManager] Arma coletada: {weaponPickup.weaponData.weaponName}");

            EquipWeapon(weaponPickup.weaponData, weaponPickup.weaponPrefab);

            WeaponSpawner spawner = collision.GetComponentInParent<WeaponSpawner>();
            if (spawner != null) {
                Debug.Log($"[WeaponManager] Notificando spawner: {spawner.gameObject.name}");
                spawner.NotifyWeaponPickedUp();
            }
            else {
                Debug.LogWarning("[WeaponManager] Nenhum WeaponSpawner encontrado no parent da arma!");
            }

            Destroy(collision.gameObject);
        }
    }
}
