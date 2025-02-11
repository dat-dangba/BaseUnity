using System.Collections;
using System.Collections.Generic;
using Teo.AutoReference;
using UnityEngine;

public class TestSpawner : BaseSpawner<ComponentSpawner, TestSpawner>
{
    [SerializeField, FindInAssets, Path("Assets/BaseUnity/Tests/_Spawner/ComponentSpawner.prefab")]
    private ComponentSpawner prefab;

    protected override ComponentSpawner GetPrefab()
    {
        return prefab;
    }

    public void Test()
    {

    }
}
