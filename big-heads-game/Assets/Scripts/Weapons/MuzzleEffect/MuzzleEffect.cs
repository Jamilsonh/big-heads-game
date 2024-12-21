using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleEffect : MonoBehaviour
{
    public GameObject muzzlePrefab;
    public Transform muzzlePoint;

    public void ShowEffect() {
        if (muzzlePrefab != null && muzzlePoint != null) {
            // Instancia o efeito e o define como filho do muzzlePoint
            GameObject effect = Instantiate(muzzlePrefab, muzzlePoint.position, muzzlePoint.rotation, muzzlePoint);

            // Destroi o efeito após um tempo (ainda será destruído mesmo como filho)
            Destroy(effect, 0.1f);
        }
    }
}
