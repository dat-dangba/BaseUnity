using System;
using System.Collections;
using System.Collections.Generic;
using Teo.AutoReference;
using UnityEngine;

public class UIManager : BaseUIManager
{
    [SerializeField, Get]
    private Transform parent;

    protected override Transform GetParent()
    {
        return parent;
    }

    protected override string GetFolderPrefabs()
    {
        return "Assets/UI";
    }

    [ContextMenu("Test")]
    private void Test()
    {
        Debug.Log($"datdb - {IsUIOnTop<OneUI>()}");
    }
}
