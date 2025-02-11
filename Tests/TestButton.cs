using System.Collections;
using System.Collections.Generic;
using Teo.AutoReference;
using TMPro;
using UnityEngine;

public class TestButton : BaseButton
{
    [SerializeField, GetInParent]
    private Canvas canvas;

    [SerializeField, GetInChildren]
    private TextMeshProUGUI text;

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
