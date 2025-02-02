using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAnimationManager : MonoBehaviour
{
    public Transform reloadPosition;               // Posição onde a animação será exibida
    public float reloadAnimationDuration = 1.75f;   // Duração da animação (em segundos)

    /// <summary>
    /// Mostra uma animação de reload específica.
    /// </summary>
    /// <param name="customAnimationPrefab">Prefab da animação personalizada (opcional).</param>
    public void TriggerReloadAnimation(GameObject customAnimationPrefab) {
        if (customAnimationPrefab != null && reloadPosition != null) {
            // Instancia a animação na posição especificada
            GameObject animationInstance = Instantiate(customAnimationPrefab, reloadPosition.position, Quaternion.identity);
            animationInstance.transform.SetParent(reloadPosition);

            // Destroi a animação após a duração especificada
            Destroy(animationInstance, reloadAnimationDuration);
        }
        else {
            Debug.LogWarning("Reload animation prefab or position is not set!");
        }
    }
}
