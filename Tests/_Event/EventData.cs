using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventData
{
    public DailyEventData DailyEventData = new();
    public WeeklyEventData WeeklyEventData = new();
    public MonthlyEventData MonthlyEventData = new();
    public CustomEventData CustomEventData = new();
}

[System.Serializable]
public class DailyEventData : BaseEventData
{

}

[System.Serializable]
public class WeeklyEventData : BaseEventData
{

}

[System.Serializable]
public class MonthlyEventData : BaseEventData
{

}

[System.Serializable]
public class CustomEventData : BaseEventData
{

}