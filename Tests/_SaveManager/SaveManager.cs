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
}
