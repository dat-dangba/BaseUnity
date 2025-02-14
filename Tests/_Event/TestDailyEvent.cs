using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDailyEvent : DailyEvent
{
    private DailyEventData data;

    protected override void GetDataEvent()
    {
        data = SaveManager.Instance.GetEventData<DailyEventData>();
    }

    protected override void NextEvent(int numberOfEvent)
    {
        Debug.Log($"datdb - Next Event Daily");
        data.NumberOfEvent = numberOfEvent;
    }

    protected override int NumberOfEvent()
    {
        return data.NumberOfEvent;
    }
}