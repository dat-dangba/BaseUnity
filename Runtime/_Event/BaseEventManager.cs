using System;
using System.Collections.Generic;
using Teo.AutoReference;
using UnityEngine;

public abstract class BaseEventManager<I> : BaseMonoBehaviour where I : MonoBehaviour
{
    [SerializeField, GetInChildren] private List<BaseEvent> events;
    [Space(10)]
    [SerializeField] private List<BaseEvent> dailyEvents;
    [SerializeField] private List<BaseEvent> weeklyEvents;
    [SerializeField] private List<BaseEvent> monthlyEvents;
    [SerializeField] private List<BaseEvent> otherEvents;

    private TimeData timeData;

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

    protected override void OnAfterSyncAttribute()
    {
        base.OnAfterSyncAttribute();
        dailyEvents = events.FindAll(e => e is DailyEvent);
        weeklyEvents = events.FindAll(e => e is WeeklyEvent);
        monthlyEvents = events.FindAll(e => e is MonthlyEvent);
        otherEvents = events.FindAll(e => e is not DailyEvent && e is not WeeklyEvent && e is not MonthlyEvent);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        TimeRequest.OnTimeRequestSuccess += CheckTime;
        TimeManager.OnTimeUpdate += CheckTime;
        TimeManager.OnNextDay += CheckTime;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        TimeRequest.OnTimeRequestSuccess -= CheckTime;
        TimeManager.OnTimeUpdate -= CheckTime;
        TimeManager.OnNextDay -= CheckTime;
    }

    protected virtual void CheckTime()
    {
        timeData ??= GetTimeData();

        int totalDays = TimeManager.Instance.GetTotalDays();
        int day = timeData.Day;
        CheckNextDay(totalDays, day);
        CheckNextWeek(totalDays, day);
        CheckNextMonth(totalDays, day);

        CheckEventStatus(otherEvents, false);
    }

    protected virtual void CheckNextDay(int totalDays, int day)
    {
        bool isNextDay = totalDays > day;

        timeData.Day = totalDays;
        CheckEventStatus(dailyEvents, isNextDay);
    }

    protected virtual void CheckNextWeek(int totalDays, int day)
    {
        int weekOfYear = TimeManager.Instance.GetWeekOfYear();
        bool isNexWeek = totalDays > day && weekOfYear != timeData.Week;

        timeData.Week = weekOfYear;
        CheckEventStatus(weeklyEvents, isNexWeek);
    }

    protected virtual void CheckNextMonth(int totalDays, int day)
    {
        int monthOfYear = TimeManager.Instance.GetMonthInYear();

        bool isNexMonth = totalDays > day && monthOfYear != timeData.Month;

        timeData.Month = monthOfYear;
        CheckEventStatus(monthlyEvents, isNexMonth);
    }

    protected virtual void CheckEventStatus(List<BaseEvent> baseEvents, bool isNextEvent)
    {
        foreach (var item in baseEvents)
        {
            item.CheckEventStatus();
            if (isNextEvent)
            {
                item.NextEvent();
            }
        }
    }

    protected abstract TimeData GetTimeData();
}
