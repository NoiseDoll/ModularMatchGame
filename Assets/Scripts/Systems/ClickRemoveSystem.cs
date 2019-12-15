using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRemoveSystem
{
    public void Execute(Board board, Vector3 worldPoint)
    {
        var point = InverseTransform(board, worldPoint);

        if (IsInBounds(board, point))
        {
            board.DespawnItem(point);
        }
    }

    private Vector2Int InverseTransform(Board board, Vector3 position)
    {
        var x = (int)(position.x + board.Width / 2f);
        var y = (int)(-position.y + board.Height / 2f);

        return new Vector2Int(x, y);
    }

    private bool IsInBounds(Board board, Vector2Int position)
    {
        return position.x >= 0 && position.x < board.Width && position.y >= 0 && position.y < board.Height;
    }
}
