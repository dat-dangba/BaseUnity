using Teo.AutoReference;
using UnityEditor;
using UnityEngine;

public abstract class BaseMonoBehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        AutoReference.Sync(this);
    }

    [OnAfterSync]
    protected virtual void OnAfterSyncAttribute()
    {
        LoadComponents();
        ResetValue();
    }

    protected virtual void LoadComponents()
    {

    }

    protected virtual void ResetValue()
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
