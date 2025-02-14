using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonthlyEvent : MonthlyEvent
{
    private MonthlyEventData data;

    protected override void GetDataEvent()
    {
        data = SaveManager.Instance.GetEventData<MonthlyEventData>();
    }

    protected override void NextEvent(int numberOfEvent)
    {
        Debug.Log($"datdb - Next Event Monthly");
        data.NumberOfEvent = numberOfEvent;
    }

    protected override int NumberOfEvent()
    {
        return data.NumberOfEvent;
    }
}