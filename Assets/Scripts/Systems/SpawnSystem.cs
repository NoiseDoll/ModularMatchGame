﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem
{
    public bool Execute(Board board)
    {
        var result = false;
        foreach (var spawner in board.Spawners)
        {
            if (spawner != null)
            {
                var pos = spawner.Position;
                if (!IsOccupied(pos, board))
                {
                    var item = new Item
                    {
                        Config = board.ItemKinds[Random.Range(0, board.ItemKinds.Count)],
                        Position = pos,
                    };

                    board.SpawnItem(item);
                    result = true;
                }
            }
        }
        Debug.Log("SpawnSyste: Execute");
        return result;
    }

    private bool IsOccupied(Vector2Int pos, Board board)
    {
        return board.Items[pos.x, pos.y] != null;
    }
}
