using System.Diagnostics;
using Teo.AutoReference;
using TMPro;
using UnityEngine;

public class TestButton : BaseButton
{
    [SerializeField, GetInParent] private Canvas canvas;

    [SerializeField, GetInChildren] private TextMeshProUGUI text;

    private bool b;

    protected override void OnClick()
    {
        //if (!b)
        //{
        //    UIManager.Instance.Show<TwoUI>();
        //    UIManager.Instance.Show<OneUI>();
        //}
        //else
        //{
        //    UIManager.Instance.HideAllIgnore<OneUI>();
        //}
        //b = !b;


        // Stopwatch stopwatch = new();
        // stopwatch.Start();
        //
        // // Gọi hàm cần đo thời gian
        // SaveManager.Instance.SaveData();
        //
        // stopwatch.Stop();
        // UnityEngine.Debug.Log($"datdb - {stopwatch.ElapsedMilliseconds}");
        //
        // Time.timeScale = 0;
        // UnityEngine.Debug.Log($"datdb - timeScale {Time.timeScale}");

        UIManager.Instance.Show<OneUI>();
    }
}