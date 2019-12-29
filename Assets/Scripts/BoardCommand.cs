using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardCommandBase
{
    public BoardElement Element;
}

public class MoveCommand : BoardCommandBase
{
    public Vector2Int FromPosition;
    public Vector2Int ToPosition;
}

public class SpawnCommand : BoardCommandBase
{
    public Vector2Int SpawnPosition;
}

public class DespawnCommand : BoardCommandBase { }
