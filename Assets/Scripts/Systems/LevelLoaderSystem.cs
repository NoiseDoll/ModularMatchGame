using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoaderSystem
{
    private readonly MatchAssetManager assetManager;
    public LevelLoaderSystem(MatchAssetManager assetManager)
    {
        this.assetManager = assetManager;
    }

    public void Execute(Board board)
    {
        var cellConfig = assetManager.GetAsset("CellView");
        var spawnerConfig = assetManager.GetAsset("SpawnerView");

        board.Create(4, 4);

        board.ItemKinds.Add((ItemKind)assetManager.GetAsset("Marble1"));
        board.ItemKinds.Add((ItemKind)assetManager.GetAsset("Marble2"));

        for (int y = 0; y < board.Height; y++)
        {
            for (int x = 0; x < board.Width; x++)
            {
                var cell = new Cell
                {
                    Config = cellConfig,
                    Position = new Vector2Int(x, y),
                };
                board.SpawnCell(cell);
            }
        }

        for (int x = 0; x < board.Width; x++)
        {
            var spawner = new Spawner
            {
                Config = spawnerConfig,
                Position = new Vector2Int(x, 0),
            };

            board.SpawnSpawner(spawner);
        }
    }
}
