using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoUI : BaseUI
{
    public void CloseUI()
    {
        UIManager.Instance.Hide<TwoUI>();
    }
}
