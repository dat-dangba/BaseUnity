using Teo.AutoReference;
using UnityEditor;
using UnityEngine;

public abstract class BaseMonoBehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        LoadComponents();
        ResetValue();
        AutoReference.Sync(this);
    }

    protected virtual void LoadComponents()
    {

    }

    protected virtual void ResetValue()
    {

    }

    [OnAfterSync]
    protected virtual void OnAfterSyncAttribute()
    {

    }

    protected virtual void Awake()
    {

    }

    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {

    }

    protected T LoadAssetAtPath<T>(string assetPath) where T : Object
    {
#if UNITY_EDITOR
        return AssetDatabase.LoadAssetAtPath<T>(assetPath);
#else
        return null;
#endif
    }
}
