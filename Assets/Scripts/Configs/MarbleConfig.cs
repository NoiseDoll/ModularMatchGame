using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Marble", menuName = "MatchGame/Marble")]
public class MarbleConfig : ItemConfig
{
    public string Name;
    public Color Color;

    public override void Click(Board board, Item item)
    {
        board.DespawnItem(item.Position);
    }
}
