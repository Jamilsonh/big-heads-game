using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffect : MonoBehaviour
{
    public GameObject fireEffectPrefab;
    public Transform firePoint;

    public void ShowFireEffect() 
    {
        if (fireEffectPrefab != null && firePoint != null) {
            GameObject effect = Instantiate(fireEffectPrefab, firePoint.position, firePoint.rotation);
            Destroy(effect, 0.01f);
        }
    }
}
