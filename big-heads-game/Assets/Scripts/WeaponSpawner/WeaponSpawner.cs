using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour {
    [SerializeField] private GameObject[] weaponPrefabs; // Lista de armas disponíveis para spawn
    [SerializeField] private float spawnTime = 5f; // Tempo fixo para spawnar uma nova arma

    private GameObject spawnedWeapon; // Armazena a arma spawnada

    private void Start() {
        StartCoroutine(SpawnWeapon());
    }

    private IEnumerator SpawnWeapon() {
        Debug.Log($"[{gameObject.name}] Iniciando spawn de arma em {spawnTime} segundos...");
        yield return new WaitForSeconds(spawnTime);

        if (spawnedWeapon == null && weaponPrefabs.Length > 0) {
            int randomIndex = Random.Range(0, weaponPrefabs.Length);
            spawnedWeapon = Instantiate(weaponPrefabs[randomIndex], transform.position, Quaternion.identity, transform); // Define o Spawner como parent

            Debug.Log($"[{gameObject.name}] Spawnou arma: {spawnedWeapon.name}");
        }
    }

    public void NotifyWeaponPickedUp() {
        Debug.Log($"[{gameObject.name}] Arma coletada! Reiniciando o spawn...");
        spawnedWeapon = null;
        StartCoroutine(SpawnWeapon()); // Reinicia o spawn após o tempo definido
    }
}