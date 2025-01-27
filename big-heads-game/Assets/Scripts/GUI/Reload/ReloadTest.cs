using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadTest : MonoBehaviour
{
    public GameObject reloadAnimationPrefab; // Prefab da anima��o de reload
    public Transform reloadPosition; // GameObject vazio onde a anima��o aparecer�
    public float reloadDuration = 1.5f; // Dura��o de exemplo para o reload

    private GameObject currentReloadAnimation; // Refer�ncia � anima��o atual

    void Update() {
        // Verifica se a tecla T foi pressionada
        if (Input.GetKeyDown(KeyCode.T)) {
            TriggerReloadAnimation();
        }
    }

    void TriggerReloadAnimation() {
        // Se uma anima��o j� estiver ativa, n�o cria outra
        if (currentReloadAnimation == null) {
            // Instancia a anima��o na posi��o do GameObject vazio
            currentReloadAnimation = Instantiate(reloadAnimationPrefab, reloadPosition.position, Quaternion.identity);

            // Define o GameObject vazio como pai da anima��o (opcional, para que siga o transform)
            currentReloadAnimation.transform.SetParent(reloadPosition);

            // Destroi a anima��o ap�s a dura��o do reload
            Destroy(currentReloadAnimation, reloadDuration);
        }
    }

     


}
