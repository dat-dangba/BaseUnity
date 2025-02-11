using System;
using System.Collections;
using System.Globalization;
using Teo.AutoReference;
using UnityEngine;

public enum TimeType
{
    UTC, LOCAL
}

public class TimeManager : Singleton<TimeManager>
{
    [SerializeField] private bool isUseLocalTime;
    [SerializeField] private bool isAutoRequest;
    [SerializeField, Get] private TimeRequest timeRequest;

    protected DateTimeOffset dateTimeOffset;
    protected DateTimeOffset startDateTimeOffset;

    protected double realTimeSinceStartup = 0;

    protected bool isInitialed;

    private readonly DateTime timeStart = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    protected string currentDay;

    public static event Action OnTimeUpdate;
    public static event Action OnNextDay;

    public bool IsInitialed { get => isInitialed; private set => isInitialed = value; }
    public DateTimeOffset DateTimeOffset { get => dateTimeOffset; set => dateTimeOffset = value; }

    protected override void ResetValue()
    {
        base.ResetValue();
        dontDestroyOnLoad = true;
    }

    protected override void Start()
    {
        base.Start();
        if (isAutoRequest)
        {
            Request();
        }
    }

    public virtual void Request()
    {
        Debug.Log($"datdb - Request {isInitialed}");
        if (!isInitialed)
        {
            timeRequest.Request();
        }
    }

    protected override void Update()
    {
        base.Update();

        if (!isInitialed)
        {
            return;
        }

        if (isUseLocalTime)
        {
            dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((long)GetTotalSeconds(DateTime.UtcNow));
        }
        else
        {
            dateTimeOffset = startDateTimeOffset.AddSeconds(Time.realtimeSinceStartupAsDouble - realTimeSinceStartup);
        }

        if (currentDay != GetDayString(GetDateTime(TimeType.LOCAL)))
        {
            currentDay = GetDayString(GetDateTime(TimeType.LOCAL));
            OnNextDay?.Invoke();
        }
    }

    protected virtual void OnApplicationFocus(bool focus)
    {
        if (focus && isInitialed)
        {
            StartCoroutine(ApplicationFocusCoroutine());
        }
    }

    private IEnumerator ApplicationFocusCoroutine()
    {
        yield return new WaitForEndOfFrame();
        OnTimeUpdate?.Invoke();
    }

    public virtual void Init(double seconds)
    {
        if (isUseLocalTime)
        {
            seconds = GetTotalSeconds(DateTime.UtcNow);
        }
        realTimeSinceStartup = Time.realtimeSinceStartupAsDouble;
        startDateTimeOffset = dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((long)seconds);
        currentDay = GetDayString(GetDateTime(TimeType.LOCAL));

        isInitialed = true;
    }

    public virtual int GetTotalDays(TimeType type = TimeType.LOCAL)
    {
        return (int)(GetDateTime(type) - timeStart).TotalDays;
    }

    public virtual int GetTotalDays(DateTime dateTime)
    {
        return (int)(dateTime - timeStart).TotalDays;
    }

    public virtual double GetTotalSeconds(DateTime dateTime)
    {
        return (dateTime - timeStart).TotalSeconds;
    }

    public virtual DateTime GetDateTime(TimeType type = TimeType.LOCAL)
    {
        return type == TimeType.UTC ? dateTimeOffset.DateTime : dateTimeOffset.LocalDateTime;
    }

    public virtual double GetCurrentTime(TimeType type = TimeType.LOCAL)
    {
        return (GetDateTime(type) - timeStart).TotalSeconds;
    }

    public virtual int GetDayOfWeek(TimeType type = TimeType.LOCAL)
    {
        return (int)GetDateTime(type).DayOfWeek;
    }

    public virtual int GetDayOfMonth(TimeType type = TimeType.LOCAL)
    {
        return GetDateTime(type).Day;
    }

    public virtual int GetDayOfYear(TimeType type = TimeType.LOCAL)
    {
        return GetDateTime(type).DayOfYear;
    }

    public virtual int GetWeekOfYear(TimeType type = TimeType.LOCAL, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
    {
        CultureInfo cultureInfo = CultureInfo.CurrentCulture;
        Calendar calendar = cultureInfo.Calendar;
        int weekOfYear = calendar.GetWeekOfYear(GetDateTime(type), cultureInfo.DateTimeFormat.CalendarWeekRule, firstDayOfWeek);
        return weekOfYear;
    }

    public virtual int GetMonthInYear(TimeType type = TimeType.LOCAL)
    {
        return GetDateTime(type).Month;
    }

    private string GetDayString(DateTime dateTime)
    {
        return $"{dateTime.Day}/{dateTime.Month}/{dateTime.Year}";
    }
}
