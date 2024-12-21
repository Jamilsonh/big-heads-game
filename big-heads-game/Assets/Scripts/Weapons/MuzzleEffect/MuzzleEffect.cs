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

            // Destroi o efeito ap�s um tempo (ainda ser� destru�do mesmo como filho)
            Destroy(effect, 0.1f);
        }
    }
}
