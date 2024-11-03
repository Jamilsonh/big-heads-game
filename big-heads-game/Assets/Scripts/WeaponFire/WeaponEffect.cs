using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffect : MonoBehaviour {
    public GameObject fireEffectPrefab; // Prefab do efeito de tiro (sprite de faísca)
    public Transform firePoint; // Ponto de saída do projétil na arma
    public float effectDuration = 0.1f; // Tempo de duração do efeito de faísca

    public void ShowFireEffect() {
        if (fireEffectPrefab != null && firePoint != null) {
            // Instancia o efeito de tiro como um filho temporário do Fire Point
            GameObject effect = Instantiate(fireEffectPrefab, firePoint.position, firePoint.rotation, firePoint);

            // Destrói o efeito após o tempo definido
            Destroy(effect, effectDuration);
        }
        else {
            Debug.LogWarning("fireEffectPrefab ou firePoint não estão atribuídos no WeaponEffect");
        }
    }
}
