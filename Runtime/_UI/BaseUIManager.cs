using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public abstract class BaseUIManager<I> : BaseMonoBehaviour where I : BaseMonoBehaviour
{
    [SerializeField] protected bool dontDestroyOnLoad = false;
    [SerializeField] private List<BaseUI> prefabs;

    protected Dictionary<Type, BaseUI> uiPrefabs = new();

    protected Dictionary<Type, BaseUI> uiLoadeds = new();

    protected abstract Transform GetParent();

    protected abstract string GetFolderPrefabs();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadUIPrefabs();
    }

    private void LoadUIPrefabs()
    {
        prefabs = new();

#if UNITY_EDITOR
        string[] files = Directory.GetFiles(GetFolderPrefabs(), "*.prefab");

        foreach (string file in files)
        {
            BaseUI prefab = AssetDatabase.LoadAssetAtPath(file, typeof(BaseUI)) as BaseUI;
            if (prefab != null)
            {
                prefabs.Add(prefab);
            }
        }
#endif
    }

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
            instance = this as I;
            if (dontDestroyOnLoad)
            {
                Transform root = transform.root;
                if (root != transform)
                {
                    DontDestroyOnLoad(root);
                }
                else
                {
                    DontDestroyOnLoad(gameObject);
                }
            }

            CreateUIPrefabs();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    private void CreateUIPrefabs()
    {
        uiPrefabs = new();
        foreach (var item in prefabs)
        {
            uiPrefabs[item.GetType()] = item;
        }
    }

    /*
     * Hiển thị U
     */
    public virtual T Show<T>() where T : BaseUI
    {
        T ui = GetUI<T>();
        ui.SetUp();
        ui.Show();

        ui.transform.SetSiblingIndex(GetParent().childCount - 1);
        return ui;
    }

    public virtual void Show<T>(Action<T> beforeShow, Action<T> afterShow = null) where T : BaseUI
    {
        T ui = GetUI<T>();
        beforeShow?.Invoke(ui);
        ui.SetUp();
        ui.Show();

        ui.transform.SetSiblingIndex(GetParent().childCount - 1);

        afterShow?.Invoke(ui);
    }

    /*
     * Ẩn UI show khoảng thời gian delayTime mặc định = 0
     */
    public virtual void Hide<T>(float delayTime = 0) where T : BaseUI
    {
        if (IsUILoaded<T>())
        {
            uiLoadeds[typeof(T)].Hide(delayTime);
        }
    }

    /*
     * Kiểm tra UI đã được tạo hay chưa
     */
    public virtual bool IsUILoaded<T>() where T : BaseUI
    {
        return uiLoadeds.ContainsKey(typeof(T)) && uiLoadeds[typeof(T)] != null;
    }

    /*
     * Kiểm tra UI đã được hiển thị chưa
     */
    public virtual bool IsUIDisplayed<T>() where T : BaseUI
    {
        return IsUILoaded<T>() && uiLoadeds[typeof(T)].gameObject.activeSelf;
    }

    /*
     * Kiểm tra chỉ show một mình UI
     */
    public virtual bool IsUIOnlyDisplay<T>() where T : BaseUI
    {
        bool isOnlyDisplay = false;

        foreach (var item in uiLoadeds)
        {
            if (item.Value != null && item.Value.gameObject.activeSelf)
            {
                if (item.Key == typeof(T))
                {
                    isOnlyDisplay = true;
                }
                else
                {
                    return false;
                }
            }
        }

        return isOnlyDisplay;
    }

    /*
     * Kiểm tra UI có hiển thị trên cùng không
     */
    public virtual bool IsUIOnTop<T>() where T : BaseUI
    {
        Transform parent = GetParent();
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            if (parent.GetChild(i).gameObject.activeSelf)
            {
                if (parent.GetChild(i).TryGetComponent<BaseUI>(out var ui))
                {
                    return uiLoadeds.ContainsKey(ui.GetType()) && ui.GetType() == typeof(T);
                }
            }
        }

        return false;
    }

    /*
     * Get UI
     */
    public virtual T GetUI<T>() where T : BaseUI
    {
        if (IsUILoaded<T>()) return uiLoadeds[typeof(T)] as T;
        T prefab = GetUIPrefab<T>();
        T ui = Instantiate(prefab, GetParent());
        ui.name = prefab.name;

        uiLoadeds[typeof(T)] = ui;

        return uiLoadeds[typeof(T)] as T;
    }

    private T GetUIPrefab<T>() where T : BaseUI
    {
        return uiPrefabs[typeof(T)] as T;
    }

    /*
     * Ẩn toàn bộ UI
     */
    public virtual void HideAll()
    {
        foreach (var item in uiLoadeds)
        {
            if (item.Value != null && item.Value.gameObject.activeSelf)
            {
                item.Value.Hide();
            }
        }
    }

    /*
     * Ẩn toàn bộ UI trừ T
     */
    public virtual void HideAllIgnore<T>() where T : BaseUI
    {
        foreach (var item in uiLoadeds)
        {
            if (item.Value != null && item.Value.gameObject.activeSelf && item.Value.GetType() != typeof(T))
            {
                item.Value.Hide();
            }
        }
    }
}