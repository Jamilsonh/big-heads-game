using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleEffect : MonoBehaviour
{
    public GameObject muzzlePrefab;
    public Transform muzzlePoint;

    public void ShowEffect() {
        if (muzzlePrefab != null && muzzlePoint != null) {
            GameObject effect = Instantiate(muzzlePrefab, muzzlePoint.position, muzzlePoint.rotation);
            Destroy(effect, 1f);
        }
    }
}
