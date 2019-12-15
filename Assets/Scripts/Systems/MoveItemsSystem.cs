using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveItemsSystem
{
    public bool Execute(Board board)
    {
        var result = false;
        foreach (var item in board.Items)
        {
            if (item != null)
            {
                var pos = item.Position;
                var down = pos + Vector2Int.up;

                if (IsWithinBoard(down, board) && IsCellExists(down, board) && !IsOccupied(down, board))
                {
                    board.MoveItem(item, down);
                    result = true;
                }
            }
        }
        Debug.Log("MoveSystem: Execute");
        return result;
    }

    private bool IsWithinBoard(Vector2Int pos, Board board)
    {
        return pos.y < board.Height;
    }

    private bool IsCellExists(Vector2Int pos, Board board)
    {
        return board.Cells[pos.x, pos.y] != null;
    }

    private bool IsOccupied(Vector2Int pos, Board board)
    {
        return board.Items[pos.x, pos.y] != null;
    }
}
