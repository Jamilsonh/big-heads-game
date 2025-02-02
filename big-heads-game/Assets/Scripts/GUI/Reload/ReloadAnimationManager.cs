using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAnimationManager : MonoBehaviour
{
    public Transform reloadPosition;               // Posi��o onde a anima��o ser� exibida
    public float reloadAnimationDuration = 1.75f;   // Dura��o da anima��o (em segundos)

    /// <summary>
    /// Mostra uma anima��o de reload espec�fica.
    /// </summary>
    /// <param name="customAnimationPrefab">Prefab da anima��o personalizada (opcional).</param>
    public void TriggerReloadAnimation(GameObject customAnimationPrefab) {
        if (customAnimationPrefab != null && reloadPosition != null) {
            // Instancia a anima��o na posi��o especificada
            GameObject animationInstance = Instantiate(customAnimationPrefab, reloadPosition.position, Quaternion.identity);
            animationInstance.transform.SetParent(reloadPosition);

            // Destroi a anima��o ap�s a dura��o especificada
            Destroy(animationInstance, reloadAnimationDuration);
        }
        else {
            Debug.LogWarning("Reload animation prefab or position is not set!");
        }
    }
}
