using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonthlyEvent
{
    public override void NextEvent()
    {
        Debug.Log($"datdb - NextEvent");
    }
}
