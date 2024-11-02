using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursorUI : MonoBehaviour
{
    public RectTransform cursorRectTransform; // Referência para o RectTransform da mira
    public Image cursorImage;
    public Color normalColor = Color.white;
    public Color shootingColor = Color.green;
    private WeaponManager weaponManager;

    void Start() {
        // Esconde o cursor padrão do sistema
        Cursor.visible = false;
        cursorImage.color = normalColor;

        // Encontra o WeaponManager na cena
        weaponManager = FindObjectOfType<WeaponManager>();
    }

    void Update() {
        // Atualiza a posição da mira para onde o mouse está na tela
        Vector2 cursorPos = Input.mousePosition;
        cursorRectTransform.position = cursorPos;

        // Altera a cor da mira com base no estado de tiro do WeaponManager
        if (weaponManager != null && weaponManager.isShooting) {
            cursorImage.color = shootingColor;
        }
        else {
            cursorImage.color = normalColor;
        }
    }
}
