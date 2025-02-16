using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataSave
{
    public int Score;
    public EventData EventData = new();
}

[System.Serializable]
public class EventData
{
    public DailyData DailyData = new();
    public WeeklyData WeeklyData = new();
    public MonthlyData MonthlyData = new();
    public CustomData CustomData = new();
}

[System.Serializable]
public class DailyData : BaseEventData
{

}

[System.Serializable]
public class WeeklyData : BaseEventData
{

}

[System.Serializable]
public class MonthlyData : BaseEventData
{

}

[System.Serializable]
public class CustomData : BaseEventData
{

}

