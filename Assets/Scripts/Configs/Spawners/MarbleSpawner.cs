using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Marble Spawner", menuName = "MatchGame/Spawners/Marble Spawner")]
public class MarbleSpawner : SpawnerConfig
{
    public override ItemConfig GenerateItem(Board board)
    {
        return board.ItemKinds[Random.Range(0, board.ItemKinds.Count)];
    }
}
