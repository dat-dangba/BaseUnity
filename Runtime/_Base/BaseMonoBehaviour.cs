using Teo.AutoReference;
using UnityEngine;

public abstract class BaseMonoBehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        LoadComponent();
        ResetValue();
        AutoReference.Sync(this);
    }

    protected virtual void LoadComponent()
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
}
