using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizerSystem
{
    private Dictionary<BoardElement, GameObject> views = new Dictionary<BoardElement, GameObject>();

    public void Execute(Board board)
    {
        var queue = board.CommandQueue;
        while (queue.Count > 0)
        {
            var command = queue.Dequeue();
            switch (command.Operation)
            {
                case "Spawn": Spawn(board, command.Element); break;
                case "Move": Move(board, command.Element); break;
                case "Despawn": Despawn(command.Element); break;
                default: throw new NotImplementedException();
            }
        }
    }

    private void Move(Board board, BoardElement item)
    {
        var view = views[item];
        view.transform.position = Transform(board, item.Position);
    }

    private void Spawn(Board board, BoardElement item)
    {
        var prefab = item.Config.View;
        var instance = UnityEngine.Object.Instantiate(prefab);
        instance.transform.position = Transform(board, item.Position);
        views.Add(item, instance);
    }
    private void Despawn(BoardElement element)
    {
        var view = views[element];
        views.Remove(element);
        UnityEngine.Object.Destroy(view);
    }

    private Vector3 Transform(Board board, Vector2Int position)
    {
        var x = position.x - (board.Width / 2f) + 0.5f;
        var y = -position.y + (board.Height / 2f) - 0.5f;
        return new Vector3(x, y, 0);
    }
}
