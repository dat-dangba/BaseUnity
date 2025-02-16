using System;
using System.Reflection;
using UnityEngine;

public class SaveManager : BaseSaveManager<SaveManager, DataSave>
{
    protected override void Reset()
    {
        base.Reset();
        ClearSave();
    }

    [ContextMenu("Clear Save")]
    public override void ClearSave()
    {
        base.ClearSave();
    }

    [ContextMenu("Save Data")]
    public override void SaveData()
    {
        base.SaveData();
    }

    protected override DataSave GetDefaultData()
    {
        return new DataSave();
    }

    public D GetEventData<D>() where D : BaseEventData
    {
        EventData eventData = DataSave.EventData;
        FieldInfo[] fields = typeof(EventData).GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(D))
            {
                return field.GetValue(eventData) as D;
            }
        }
        return null;
    }
}
