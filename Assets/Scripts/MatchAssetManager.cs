﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Asset Manager", menuName = "MatchGame/AssetManager")]
public class MatchAssetManager : ScriptableObject
{
    public ElementConfig[] Assets;

    public ElementConfig GetAsset(string name)
    {
        // TODO: Obviously cache this into dictionary
        foreach (var asset in Assets)
        {
            if (asset.name.Equals(name))
            {
                return asset;
            }
        }
        return null;
    }
}
