using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffectSelfDestruction : MonoBehaviour
{
    public float destroyDelay = 5f; // Tempo em segundos antes de destruir o objeto

    private void Start() {
        Destroy(gameObject, destroyDelay); // Destroi este GameObject após o tempo especificado
    }
}
 