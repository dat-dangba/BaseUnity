using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

public class SaveManager : BaseSaveManager<SaveManager, DataSave>
{
    [ContextMenu("Clear Data")]
    public override void ClearData()
    {
        base.ClearData();
    }

    [ContextMenu("Save Data")]
    public override void SaveData()
    {
        base.SaveData();
    }

    public T GetEventData<T>() where T : class, new()
    {
        EventData eventData = DataSave.EventData;

        FieldInfo[] fields = typeof(EventData).GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(T))
            {
                return field.GetValue(eventData) as T;
            }
        }
        return new T();
    }
}