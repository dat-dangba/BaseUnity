using System;
using System.Collections.Generic;
using Teo.AutoReference;
using UnityEngine;

public abstract class BaseEventManager<I> : BaseMonoBehaviour where I : MonoBehaviour
{
    [SerializeField, GetInChildren] private List<BaseEvent> events;

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
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    protected override void OnEnable()
    {
        base.OnEnable();
        TimeRequest.OnTimeRequestSuccess += CheckEvent;
        TimeManager.OnTimeUpdate += CheckEvent;
        TimeManager.OnNextDay += CheckEvent;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        TimeRequest.OnTimeRequestSuccess -= CheckEvent;
        TimeManager.OnTimeUpdate -= CheckEvent;
        TimeManager.OnNextDay -= CheckEvent;
    }

    protected virtual void CheckEvent()
    {
        foreach (var item in events)
        {
            item.CheckEvent();
        }
    }

    public virtual E GetEvent<E>() where E : BaseEvent
    {
        foreach (var item in events)
        {
            if (item.GetType() == typeof(E))
            {
                return item as E;
            }
        }
        return null;
    }
}
