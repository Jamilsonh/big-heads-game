using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // Velocidade de movimento do player
    public float moveSpeed = 5f;

    // Rigidbody2D do player
    private Rigidbody2D rb;

    // Vetor para armazenar a dire��o do movimento
    private Vector2 movement;

    // Animator do player
    private Animator animator;

    // Inicializa��o
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Atualiza a entrada do jogador a cada frame
    void Update() {
        // Captura a entrada do jogador
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Atualiza as anima��es
        UpdateAnimations();
    }
     
    // F�sicas e movimenta��o do player
    void FixedUpdate() {
        // Movimenta o player usando o Rigidbody2D
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    // Atualiza as anima��es do player
    private void UpdateAnimations() {
        if (animator != null) {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }
}
