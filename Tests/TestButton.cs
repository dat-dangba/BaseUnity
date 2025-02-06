using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : BaseButton
{
    private bool b = false;

    protected override void OnClick()
    {
        //if (!b)
        //{
        //    UIManager.Instance.Show<OneUI>();
        //}
        //else
        //{
        //    UIManager.Instance.Show<TwoUI>();
        //}
        //b = !b;

        //UIManager.Instance.Show<TwoUI>();
        //UIManager.Instance.Show<OneUI>();
        SaveManager.Instance.DataSave.Score = 1;
    }
}
