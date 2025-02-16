using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDailyEvent : DailyEvent
{
    //private DailyData dailyData;

    protected override void GetDataEvent()
    {
        //dailyData = SaveManager.Instance.GetEventData<DailyData>();
    }

    protected override void NextEvent(int numberOfEvent)
    {
        Debug.Log($"datdb - Next Event Daily");
        //dailyData.NumberOfEvent = numberOfEvent;
    }

    protected override int NumberOfEvent()
    {
        return 0;//dailyData.NumberOfEvent;
    }
}