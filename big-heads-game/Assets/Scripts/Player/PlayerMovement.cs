using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // Velocidade de movimento do player
    public float moveSpeed = 5f;

    // Rigidbody2D do player
    private Rigidbody2D rb;

    // Vetor para armazenar a direção do movimento
    private Vector2 movement;

    // Animator do player
    private Animator animator;

    // Inicialização
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Atualiza a entrada do jogador a cada frame
    void Update() {
        // Captura a entrada do jogador
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Atualiza as animações
        UpdateAnimations();
    }
     
    // Físicas e movimentação do player
    void FixedUpdate() {
        // Movimenta o player usando o Rigidbody2D
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    // Atualiza as animações do player
    private void UpdateAnimations() {
        if (animator != null) {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }
}
