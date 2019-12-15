using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bonus", menuName = "MatchGame/Bonus")]
public class BonusConfig : ItemConfig
{
    public override void Click(Board board, Item item)
    {
        foreach (var dir in directions)
        {
            var newPos = item.Position + dir;
            if (board.IsInBounds(newPos))
            {
                board.DespawnItem(newPos);
            }
        }
    }

    private readonly Vector2Int[] directions = new[]
    {
        Vector2Int.down,
        Vector2Int.up,
        Vector2Int.left,
        Vector2Int.right,
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 1),
        new Vector2Int(1, -1),
        Vector2Int.one,
    };
}
