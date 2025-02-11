using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DailyEvent : BaseEvent
{
    protected override TimeSpan BreakTime()
    {
        return TimeSpan.FromDays(0);
    }

    protected override TimeSpan EventDuration()
    {
        return TimeSpan.FromDays(1);
    }
}
