using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonthlyEvent : MonthlyEvent
{
    //private MonthlyData monthlyData;

    protected override void GetDataEvent()
    {
        //monthlyData = SaveManager.Instance.GetEventData<MonthlyData>();
    }

    protected override void NextEvent(int numberOfEvent)
    {
        Debug.Log($"datdb - Next Event Monthly");
        //monthlyData.NumberOfEvent = numberOfEvent;
    }

    protected override int NumberOfEvent()
    {
        return 0;// monthlyData.NumberOfEvent;
    }
}