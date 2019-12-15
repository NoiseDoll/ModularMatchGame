using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemConfig : ElementConfig
{
    public abstract void Click(Board board, Item item);
}
