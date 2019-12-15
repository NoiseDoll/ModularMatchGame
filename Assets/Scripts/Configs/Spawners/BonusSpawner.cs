using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bonus Spawner", menuName = "MatchGame/Spawners/Bonus Spawner")]
public class BonusSpawner : SpawnerConfig
{
    public BonusConfig Bonus;

    public override ItemConfig GenerateItem(Board board)
    {
        return Bonus;
    }
}
