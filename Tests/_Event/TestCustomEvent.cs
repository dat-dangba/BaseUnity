using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCustomEvent : BaseEvent
{
    private CustomEventData data;

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
        data = SaveManager.Instance.GetEventData<CustomEventData>();
    }

    protected override void NextEvent(int numberOfEvent)
    {
        Debug.Log($"datdb - Next Event Custom");
        data.NumberOfEvent = numberOfEvent;
    }

    protected override int NumberOfEvent()
    {
        return data.NumberOfEvent;
    }

    protected override DateTime GetTimeStart()
    {
        return new(2025, 2, 13, 0, 0, 0, DateTimeKind.Utc);
    }
}