using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public WeaponScriptableConfig weaponData; // Dados da arma que será equipada
    public GameObject weaponPrefab; // Prefab da arma a ser instanciada no jogador
}
