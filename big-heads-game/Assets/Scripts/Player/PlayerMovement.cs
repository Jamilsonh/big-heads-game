using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f;
    //public Transform weapon; // Referência ao transform da arma

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        // Captura a entrada do jogador
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Verifica se há movimento e ativa a animação de correr
        if (animator != null) {
            animator.SetBool("isRunning", movement.magnitude > 0);
        }

        // Ajusta a direção do player e da arma
        RotatePlayerAndWeaponTowardsMouse();

        // Testar dano e cura
        if (Input.GetKeyDown(KeyCode.K)) { // Aperte "K" para sofrer dano
            GetComponent<PlayerHealth>().TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.H)) { // Aperte "H" para curar
            GetComponent<PlayerHealth>().Heal(10);
        }
    }

    void FixedUpdate() {
        // Movimenta o player usando o Rigidbody2D
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    // Gira o player e a arma em direção ao mouse
    void RotatePlayerAndWeaponTowardsMouse() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ajusta a posição Z do mouse

        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Transform weaponHolder = GetComponentInChildren<WeaponManager>().weaponHolder;
        weaponHolder.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Vira o player e a arma para a esquerda ou direita
        if (direction.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1); // Vira o player para a esquerda
            weaponHolder.localScale = new Vector3(-1, -1, 1); // Inverte a escala Y da arma
        }
        else {
            transform.localScale = new Vector3(1, 1, 1); // Vira o player para a direita
            weaponHolder.localScale = new Vector3(1, 1, 1); // Normaliza a escala Y da arma
        }
    }
}
