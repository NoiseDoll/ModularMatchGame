using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerConfig : ElementConfig
{
    public abstract ItemConfig GenerateItem(Board board);
}
