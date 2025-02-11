using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : BaseEventManager
{
    protected override TimeData GetTimeData()
    {
        return SaveManager.Instance.DataSave.TimeData;
    }
}
