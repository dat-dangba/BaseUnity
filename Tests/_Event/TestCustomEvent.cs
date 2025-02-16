using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCustomEvent : BaseEvent
{
    //private CustomData customData;

    protected override TimeSpan BreakTime()
    {
        return TimeSpan.FromDays(1);
    }

    protected override TimeSpan EventDuration()
    {
        return TimeSpan.FromDays(1);
    }

    protected override void GetDataEvent()
    {
        //customData = SaveManager.Instance.GetEventData<CustomData>();
    }

    protected override void NextEvent(int numberOfEvent)
    {
        Debug.Log($"datdb - Next Event Custom");
        //customData.NumberOfEvent = numberOfEvent;
    }

    protected override int NumberOfEvent()
    {
        return 0;//;customData.NumberOfEvent;
    }

    protected override DateTime GetTimeStart()
    {
        return new(2025, 2, 13, 0, 0, 0, DateTimeKind.Utc);
    }
}