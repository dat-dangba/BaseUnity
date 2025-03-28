using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneUI : BaseUI
{
    public void CloseUI()
    {
        UIManager.Instance.Hide<OneUI>();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }
}