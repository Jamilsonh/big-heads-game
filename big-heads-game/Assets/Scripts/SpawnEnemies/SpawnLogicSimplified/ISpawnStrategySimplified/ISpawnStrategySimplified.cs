using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnStrategySimplified
{
    void Start(); // Inicializa a estratégia
    void Update(); // Atualiza a lógica de spawn
    void End(); // Finaliza a estratégia
    string GetName();
}
