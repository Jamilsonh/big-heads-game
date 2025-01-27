using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionPulse : MonoBehaviour
{
    public Renderer objectRenderer; // Referência ao Renderer do objeto
    public float pulseSpeed = 2f;   // Velocidade do pulso
    public Color emissionColor = Color.yellow; // Cor do brilho

    private Material material;

    void Start() {
        // Obtém o material do Renderer
        material = objectRenderer.material;

        // Habilita o emissivo no material
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", emissionColor);
    }
     
    void Update() {
        // Calcula a intensidade do brilho usando uma onda senoidal
        float intensity = Mathf.PingPong(Time.time * pulseSpeed, 1f);
        material.SetColor("_EmissionColor", emissionColor * intensity);
    }
}
