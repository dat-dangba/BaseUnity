using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeeklyEvent : WeeklyEvent
{
    private WeeklyEventData data;

    protected override void GetDataEvent()
    {
        data = SaveManager.Instance.GetEventData<WeeklyEventData>();
    }

    protected override void NextEvent(int numberOfEvent)
    {
        Debug.Log($"datdb - Next Event Weekly");
        data.NumberOfEvent = numberOfEvent;
    }

    protected override int NumberOfEvent()
    {
        return data.NumberOfEvent;
    }
}