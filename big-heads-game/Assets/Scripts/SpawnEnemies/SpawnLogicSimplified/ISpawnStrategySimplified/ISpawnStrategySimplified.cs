using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnStrategySimplified
{
    void Start(); // Inicializa a estrat�gia
    void Update(); // Atualiza a l�gica de spawn
    void End(); // Finaliza a estrat�gia
    string GetName();
}
