using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffect : MonoBehaviour {
    public GameObject fireEffectPrefab; // Prefab do efeito de tiro (sprite de fa�sca)
    public Transform firePoint; // Ponto de sa�da do proj�til na arma
    public float effectDuration = 0.1f; // Tempo de dura��o do efeito de fa�sca

    public void ShowFireEffect() {
        if (fireEffectPrefab != null && firePoint != null) {
            // Instancia o efeito de tiro como um filho tempor�rio do Fire Point
            GameObject effect = Instantiate(fireEffectPrefab, firePoint.position, firePoint.rotation, firePoint);

            // Destr�i o efeito ap�s o tempo definido
            Destroy(effect, effectDuration);
        }
        else {
            Debug.LogWarning("fireEffectPrefab ou firePoint n�o est�o atribu�dos no WeaponEffect");
        }
    }
}
