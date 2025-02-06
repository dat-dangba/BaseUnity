using Teo.AutoReference;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseMonoBehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        AutoReference.Sync(this);
    }

    [OnAfterSync]
    protected virtual void OnAfterSyncAttribute()
    {

    }

    protected virtual void Awake()
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

    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {

    }
}
