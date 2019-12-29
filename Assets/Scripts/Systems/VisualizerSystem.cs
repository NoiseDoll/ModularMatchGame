using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizerSystem
{
    private float[,] timings;

    private Dictionary<BoardElement, ElementView> views = new Dictionary<BoardElement, ElementView>();

    public VisualizerSystem(Board board)
    {
        timings = new float[board.Width, board.Height];
    }

    public void Execute(Board board)
    {
        var queue = board.CommandQueue;
        while (queue.Count > 0)
        {
            var command = queue.Dequeue();
            switch (command)
            {
                case SpawnCommand com: Spawn(board, com.Element, com.SpawnPosition); break;
                case DespawnCommand com: Despawn(com.Element); break;
                case MoveCommand com: Move(board, com.Element, com.FromPosition, com.ToPosition); break;
                default: throw new NotImplementedException();
            }
        }
    }

    private void Move(Board board, BoardElement item, Vector2Int fromPosition, Vector2Int toPosition)
    {
        var view = views[item];

        var targetTiming = timings[toPosition.x, toPosition.y];
        var delay = Mathf.Max(0, targetTiming - Time.time);
        view.Move(Transform(board, toPosition), delay);
        if (delay > 0)
        {
            timings[fromPosition.x, fromPosition.y] = targetTiming;
        }
    }

    private void Spawn(Board board, BoardElement item, Vector2Int position)
    {
        var prefab = item.Config.View;
        var instance = UnityEngine.Object.Instantiate(prefab);
        var view = instance.GetComponent<ElementView>();
        views.Add(item, view);

        if (item is Item)
        {
            var prev = position + Vector2Int.down;
            instance.transform.position = Transform(board, prev);

            var targetTiming = timings[position.x, position.y];
            var delay = Mathf.Max(0, targetTiming - Time.time);
            view.Spawn(Transform(board, position), delay);
            timings[position.x, position.y] = Time.time + view.DropTime;
        } else
        {
            instance.transform.position = Transform(board, position);
        }
    }

    private void Despawn(BoardElement element)
    {
        var view = views[element];
        views.Remove(element);
        var pos = element.Position;
        timings[pos.x, pos.y] = Time.time + view.DespawnTime;
        view.Despawn();
    }

    private static Vector3 Transform(Board board, Vector2Int position)
    {
        var x = position.x - (board.Width / 2f) + 0.5f;
        var y = -position.y + (board.Height / 2f) - 0.5f;
        return new Vector3(x, y, 0);
    }
}
