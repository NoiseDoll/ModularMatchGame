using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSystem
{
    public void Execute(Board board, Vector3 worldPoint)
    {
        var point = InverseTransform(board, worldPoint);

        if (board.IsInBounds(point))
        {
            var item = board.Items[point.x, point.y];
            item.Kind.Click(board, item);
        }
    }

    private Vector2Int InverseTransform(Board board, Vector3 position)
    {
        var x = (int)(position.x + board.Width / 2f);
        var y = (int)(-position.y + board.Height / 2f);

        return new Vector2Int(x, y);
    }
}
