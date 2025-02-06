using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSaveManager<T> : BaseMonoBehaviour where T : class
{
    [SerializeField]
    protected T dataSave;
    public T DataSave => dataSave;

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveData();
        }
    }

    protected void Init()
    {
        SaveSystem.Initialize();
        LoadData();
    }

    private void LoadData()
    {
        string jsonString = SaveSystem.GetString(GetSaveGame());
        dataSave = JsonUtility.FromJson<T>(jsonString);
        dataSave ??= GetDefaultData();
    }

    [ContextMenu("Clear")]
    public virtual void ClearSave()
    {
        dataSave = GetDefaultData();
        SaveSystem.Clear();
        SaveData();
    }

    [ContextMenu("Save Data")]
    public virtual void SaveData()
    {
        dataSave ??= GetDefaultData();
        string json = JsonUtility.ToJson(dataSave);
        SaveSystem.SetString(GetSaveGame(), json);
        SaveSystem.SaveToDisk();
    }

    protected virtual string GetSaveGame()
    {
        return "save_game";
    }

    protected abstract T GetDefaultData();
}
