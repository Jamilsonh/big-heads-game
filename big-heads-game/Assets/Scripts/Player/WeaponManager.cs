using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform weaponHolder; // Objeto onde a nova arma ser� equipada
    public Weapon currentWeapon; // Refer�ncia � arma equipada atualmente
    public bool isShooting; // Indica se o jogador est� atirando
    public WeaponEffect weaponEffect; // Referencia ao script de efeito de tiro

    void Update() {
        // Detecta o clique do mouse ou bot�o de tiro
        if (Input.GetButton("Fire1") && currentWeapon != null) {
            currentWeapon.Use();
            weaponEffect.ShowFireEffect(); // Chama o efeito de tiro
            isShooting = true; // Define isShooting como true enquanto o jogador est� atirando
        }
        else {
            isShooting = false; // Define isShooting como false quando o jogador para de atirar
        }
    }

    // M�todo para equipar uma nova arma
    public void EquipWeapon(GameObject weaponPrefab) {
        // Se j� existir uma arma equipada, destr�i ela
        if (currentWeapon != null) {
            Destroy(currentWeapon.gameObject);
        }

        // Instancia o novo prefab da arma no holder e configura como arma atual
        GameObject newWeapon = Instantiate(weaponPrefab, weaponHolder.position, Quaternion.identity, weaponHolder);
        newWeapon.transform.localPosition = Vector3.zero; // Alinha ao centro do holder
        newWeapon.transform.localRotation = Quaternion.identity; // Reseta a rota��o
        newWeapon.transform.localScale = Vector3.one; // Reseta a escala para evitar deforma��es

        currentWeapon = newWeapon.GetComponent<Weapon>();
    }

    // Detecta a colis�o com uma arma no ch�o e realiza a troca
    private void OnTriggerEnter2D(Collider2D collision) {
        // Verifica se o objeto que colidiu � uma arma
        Weapon weaponOnGround = collision.GetComponent<Weapon>();
        if (weaponOnGround != null) {
            EquipWeapon(weaponOnGround.gameObject); // Equipa a arma coletada
            Destroy(collision.gameObject); // Remove a arma do ch�o
        }
    }
}
