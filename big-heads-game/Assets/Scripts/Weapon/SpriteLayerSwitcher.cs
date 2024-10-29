using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayerSwitcher : MonoBehaviour
{
    public SpriteRenderer playerSpriteRenderer;
    public SpriteRenderer weaponSpriteRenderer;

    private float changeOrderTime; // Tempo para pr�xima mudan�a na ordem de renderiza��o
    private float timer; // Temporizador

    void Start() {
        // Inicializa o tempo de mudan�a
        SetRandomChangeTime();
    }

    void Update() {
        // Atualiza o temporizador para alternar a ordem de renderiza��o
        UpdateRenderOrder();
    }

    // Alterna a ordem de renderiza��o de forma aleat�ria
    void UpdateRenderOrder() {
        timer += Time.deltaTime;

        if (timer >= changeOrderTime) {
            // Alterna aleatoriamente a ordem de renderiza��o
            if (Random.value > 0.5f) {
                weaponSpriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder + 1; // Na frente
            }
            else {
                weaponSpriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder - 1; // Atr�s
            }

            // Reinicia o temporizador e define um novo intervalo aleat�rio
            SetRandomChangeTime();
            timer = 0f;
        }
    }

    // Define um intervalo aleat�rio para a pr�xima mudan�a
    void SetRandomChangeTime() {
        changeOrderTime = Random.Range(0.5f, 2f); // Intervalo aleat�rio entre 0.5 e 2 segundos
    }
}
