using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeeklyEvent : BaseEvent
{
    private readonly DateTime timeStart = new(1970, 1, 5, 0, 0, 0, DateTimeKind.Utc);

    protected override TimeSpan BreakTime()
    {
        return TimeSpan.FromDays(0);
    }

    protected override TimeSpan EventDuration()
    {
        return TimeSpan.FromDays(7);
    }

    protected override DateTime GetTimeStart()
    {
        return timeStart;
    }
}