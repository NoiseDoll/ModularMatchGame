using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public int Width;
    public int Height;

    public List<MarbleConfig> ItemKinds;

    public Cell[,] Cells;
    public Spawner[,] Spawners;
    public Item[,] Items;

    public Queue<BoardCommandBase> CommandQueue;

    public void Create(int width, int height)
    {
        Width = width;
        Height = height;
        Cells = new Cell[width, height];
        Spawners = new Spawner[width, height];
        Items = new Item[width, height];

        ItemKinds = new List<MarbleConfig>();

        CommandQueue = new Queue<BoardCommandBase>();
    }

    public void SpawnCell(Cell cell)
    {
        SpawnInternal(Cells, cell);
    }

    public void SpawnSpawner(Spawner spawner)
    {
        SpawnInternal(Spawners, spawner);
    }

    public void SpawnItem(Item item)
    {
        SpawnInternal(Items, item);
    }

    private void SpawnInternal<T>(T[,] array, T element) where T : BoardElement
    {
        var pos = element.Position;
        array[pos.x, pos.y] = element;
        var command = new SpawnCommand
        {
            Element = element,
            SpawnPosition = pos,
        };
        CommandQueue.Enqueue(command);
    }

    public void MoveItem(Item item, Vector2Int newPos)
    {
        var pos = item.Position;
        item.Position = newPos;

        Items[pos.x, pos.y] = null;
        Items[newPos.x, newPos.y] = item;

        var command = new MoveCommand
        {
            Element = item,
            FromPosition = pos,
            ToPosition = newPos,
        };
        CommandQueue.Enqueue(command);
    }

    public void SwapItems(Item item1, Item item2)
    {
        var pos1 = item1.Position;
        var pos2 = item2.Position;
        item1.Position = pos2;
        item2.Position = pos1;

        Items[pos2.x, pos2.y] = item1;
        Items[pos1.x, pos1.y] = item2;

        var command = new MoveCommand
        {
            Element = item1,
            FromPosition = pos1,
            ToPosition = pos2,
        };
        CommandQueue.Enqueue(command);
        command = new MoveCommand
        {
            Element = item2,
            FromPosition = pos2,
            ToPosition = pos1,
        };
        CommandQueue.Enqueue(command);
    }

    public void DespawnItem(Vector2Int pos)
    {
        var item = Items[pos.x, pos.y];
        Items[pos.x, pos.y] = null;

        var command = new DespawnCommand
        {
            Element = item,
        };
        CommandQueue.Enqueue(command);
    }

    public bool IsInBounds(Vector2Int position)
    {
        return position.x >= 0 && position.x < Width && position.y >= 0 && position.y < Height;
    }
}

public class BoardElement
{
    public Vector2Int Position;
    public ElementConfig Config;
}

public class Cell : BoardElement
{
}

public class Spawner : BoardElement
{
    public SpawnerConfig SpawnerConfig => Config as SpawnerConfig;
}

public class Item : BoardElement
{
    public ItemConfig Kind => Config as ItemConfig;
}
