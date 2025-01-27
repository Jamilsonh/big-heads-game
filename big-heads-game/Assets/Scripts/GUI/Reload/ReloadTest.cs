using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadTest : MonoBehaviour
{
    public GameObject reloadAnimationPrefab; // Prefab da animação de reload
    public Transform reloadPosition; // GameObject vazio onde a animação aparecerá
    public float reloadDuration = 1.5f; // Duração de exemplo para o reload

    private GameObject currentReloadAnimation; // Referência à animação atual

    void Update() {
        // Verifica se a tecla T foi pressionada
        if (Input.GetKeyDown(KeyCode.T)) {
            TriggerReloadAnimation();
        }
    }

    void TriggerReloadAnimation() {
        // Se uma animação já estiver ativa, não cria outra
        if (currentReloadAnimation == null) {
            // Instancia a animação na posição do GameObject vazio
            currentReloadAnimation = Instantiate(reloadAnimationPrefab, reloadPosition.position, Quaternion.identity);

            // Define o GameObject vazio como pai da animação (opcional, para que siga o transform)
            currentReloadAnimation.transform.SetParent(reloadPosition);

            // Destroi a animação após a duração do reload
            Destroy(currentReloadAnimation, reloadDuration);
        }
    }

     


}
