using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeeklyEvent : WeeklyEvent
{
    private WeeklyData weeklyData;

    protected override void GetDataEvent()
    {
        weeklyData = SaveManager.Instance.GetEventData<WeeklyData>();
    }

    protected override void NextEvent(int numberOfEvent)
    {
        Debug.Log($"datdb - Next Event Weekly");
        weeklyData.NumberOfEvent = numberOfEvent;
    }

    protected override int NumberOfEvent()
    {
        return weeklyData.NumberOfEvent;
    }
}