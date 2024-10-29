using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayerSwitcher : MonoBehaviour
{
    public SpriteRenderer playerSpriteRenderer;
    public SpriteRenderer weaponSpriteRenderer;

    private float changeOrderTime; // Tempo para próxima mudança na ordem de renderização
    private float timer; // Temporizador

    void Start() {
        // Inicializa o tempo de mudança
        SetRandomChangeTime();
    }

    void Update() {
        // Atualiza o temporizador para alternar a ordem de renderização
        UpdateRenderOrder();
    }

    // Alterna a ordem de renderização de forma aleatória
    void UpdateRenderOrder() {
        timer += Time.deltaTime;

        if (timer >= changeOrderTime) {
            // Alterna aleatoriamente a ordem de renderização
            if (Random.value > 0.5f) {
                weaponSpriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder + 1; // Na frente
            }
            else {
                weaponSpriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder - 1; // Atrás
            }

            // Reinicia o temporizador e define um novo intervalo aleatório
            SetRandomChangeTime();
            timer = 0f;
        }
    }

    // Define um intervalo aleatório para a próxima mudança
    void SetRandomChangeTime() {
        changeOrderTime = Random.Range(0.5f, 2f); // Intervalo aleatório entre 0.5 e 2 segundos
    }
}
