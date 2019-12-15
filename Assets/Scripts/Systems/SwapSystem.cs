using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Swap System", menuName = "MatchGame/Systems/Input/Swap System")]
public class SwapSystem : InputSystem
{
    private Vector2Int? firstClick;

    public override void Execute(Board board, Vector3 worldPosition)
    {
        var point = InverseTransform(board, worldPosition);
        if (!board.IsInBounds(point))
        {
            firstClick = null;
            return;
        }

        if (firstClick == null)
        {
            firstClick = point;
        } else if (firstClick != point)
        {
            var distance = firstClick.Value - point;
            var x = Mathf.Abs(distance.x);
            var y = Mathf.Abs(distance.y);

            if ((x == 1 && y == 0) || (x == 0 && y == 1))
            {
                var item1 = board.Items[point.x, point.y];
                var item2 = board.Items[firstClick.Value.x, firstClick.Value.y];
                board.SwapItems(item1, item2);
            }
            firstClick = null;
        }
    }

    private Vector2Int InverseTransform(Board board, Vector3 position)
    {
        var x = (int)(position.x + board.Width / 2f);
        var y = (int)(-position.y + board.Height / 2f);

        return new Vector2Int(x, y);
    }
}
