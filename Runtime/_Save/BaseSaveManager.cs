using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSaveManager<I, T> : BaseMonoBehaviour where I : MonoBehaviour where T : class, new()
{
    [SerializeField]
    private T dataSave;
    public T DataSave => dataSave;

    #region Singleton
    private static I instance;
    public static I Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<I>();
                if (instance == null)
                {
                    GameObject singleton = new(typeof(I).Name);
                    instance = singleton.AddComponent<I>();
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
            instance = GetComponent<I>();
            Transform root = transform.root;
            if (root != transform)
            {
                DontDestroyOnLoad(root);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    protected override void Reset()
    {
        base.Reset();
        ClearData();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveData();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();
        }
    }

    protected virtual void LoadData()
    {
        string data = SaveGame.LoadData();
        if (data == "")
        {
            SaveDefaultData();
        }
        else
        {
            dataSave = JsonUtility.FromJson<T>(data);
        }
    }

    private void SaveDefaultData()
    {
        dataSave = new T();
        SaveData();
    }

    public virtual void ClearData()
    {
        SaveGame.ClearData();
        SaveDefaultData();
    }

    public virtual void SaveData()
    {
        SaveGame.SaveData(JsonUtility.ToJson(dataSave));
    }
}
