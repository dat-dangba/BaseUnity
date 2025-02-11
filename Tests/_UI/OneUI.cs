using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneUI : BaseUI
{
    public void CloseUI()
    {
        Debug.Log($"datdb - 1 {UIManager.Instance.IsUIOnTop<OneUI>()}");
        UIManager.Instance.Hide<OneUI>();
        Debug.Log($"datdb - 2 {UIManager.Instance.IsUIOnTop<OneUI>()}");
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Debug.Log($"datdb - 3 {UIManager.Instance.IsUIOnTop<OneUI>()}");
    }
}
