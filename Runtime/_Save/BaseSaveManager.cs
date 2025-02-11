using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSaveManager<B, T> : BaseMonoBehaviour where B : BaseMonoBehaviour where T : class
{
    [SerializeField]
    protected T dataSave;
    public T DataSave => dataSave;

    #region Singleton
    private static B instance;
    public static B Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<B>();
                if (instance == null)
                {
                    GameObject singleton = new(typeof(B).Name);
                    instance = singleton.AddComponent<B>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return instance;
        }
    }

    protected override void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<B>();
            Transform root = transform.root;
            if (root != transform)
            {
                DontDestroyOnLoad(root);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveData();
        }
    }

    protected virtual void Init()
    {
        SaveSystem.Initialize();
        LoadData();
    }

    protected virtual void LoadData()
    {
        string jsonString = SaveSystem.GetString(GetSaveGame());
        dataSave = JsonUtility.FromJson<T>(jsonString);
        dataSave ??= GetDefaultData();
    }

    public virtual void ClearSave()
    {
        dataSave = GetDefaultData();
        SaveSystem.Clear();
        SaveData();
    }

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
