using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputSystem : SystemBase
{
    public abstract void Execute(Board board, Vector3 worldPosition);
}
