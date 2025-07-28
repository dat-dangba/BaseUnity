using System;
using System.Collections;
using UnityEngine;

public abstract class BaseEvent : BaseMonoBehaviour
{
    private readonly DateTime timeStart = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    [SerializeField] protected bool isEventHappening;
    [SerializeField] protected bool isInEvent;
    [SerializeField] protected double eventTime;
    [SerializeField] protected double nextEventTime;
    [SerializeField] protected int numberOfEvent;

    protected double EventTime
    {
        get => eventTime;
        private set => eventTime = value;
    }

    protected double NextEventTime
    {
        get => nextEventTime;
        private set => nextEventTime = value;
    }

    protected bool IsEventHappening
    {
        get => isEventHappening;
        private set => isEventHappening = value;
    }

    public bool IsInEvent
    {
        get => isInEvent;
        private set => isInEvent = value;
    }

    protected override void Start()
    {
        base.Start();
        GetDataEvent();
    }

    public virtual void CheckEvent()
    {
        DateTime startDateTime = GetTimeStart();
        DateTime currentDateTime = TimeManager.Instance.DateTimeOffset.LocalDateTime;
        TimeSpan timeElapsed = currentDateTime - startDateTime;

        numberOfEvent = GetNumberOfEvent(startDateTime, currentDateTime, timeElapsed);

        if ((GetLoop() != -1 && numberOfEvent >= GetLoop()) || timeElapsed.TotalHours < 0)
        {
            isInEvent = false;
            isEventHappening = false;
            eventTime = 0;
            nextEventTime = 0;
            return;
        }

        isInEvent = true;
        isEventHappening = (timeElapsed.TotalHours % (EventDuration() + BreakTime()).TotalHours) <
                           EventDuration().TotalHours;

        StopAllCoroutines();

        if (isEventHappening)
        {
            DateTime eventStartTime = startDateTime + TimeSpan.FromHours(
                Math.Floor(timeElapsed.TotalHours / (EventDuration() + BreakTime()).TotalHours) *
                (EventDuration() + BreakTime()).TotalHours);
            DateTime eventEndTime = eventStartTime + EventDuration();
            eventTime = (float)(eventEndTime - currentDateTime).TotalSeconds;
            StartCoroutine(CountDownRemainingTime());
        }
        else
        {
            eventTime = 0;
        }

        nextEventTime = (float)((EventDuration() + BreakTime()).TotalSeconds -
                                (timeElapsed.TotalSeconds % (EventDuration() + BreakTime()).TotalSeconds));
        StartCoroutine(CountDownTimeToNextEvent());

        if (numberOfEvent != NumberOfEvent())
        {
            NextEvent(numberOfEvent);
        }
    }

    protected IEnumerator CountDownTimeToNextEvent()
    {
        while (nextEventTime > 0)
        {
            nextEventTime -= Time.deltaTime;
            yield return null;
        }

        CheckEvent();
    }


    protected IEnumerator CountDownRemainingTime()
    {
        while (eventTime > 0)
        {
            eventTime -= Time.deltaTime;
            yield return null;
        }

        CheckEvent();
    }

    protected virtual int GetNumberOfEvent(DateTime startDateTime, DateTime currentDateTime, TimeSpan timeElapsed)
    {
        return (int)(timeElapsed.TotalHours / (EventDuration() + BreakTime()).TotalHours);
    }

    /*
     * -1 lặp lại liên tục
     * 1,2,3,4.. sô vòng lặp của event > 0
     */
    protected virtual int GetLoop()
    {
        return -1;
    }

    protected virtual DateTime GetTimeStart()
    {
        return timeStart;
    }

    protected abstract TimeSpan EventDuration();

    protected abstract TimeSpan BreakTime();

    protected abstract void GetDataEvent();

    protected abstract int NumberOfEvent();

    protected abstract void NextEvent(int numberOfEvent);
}