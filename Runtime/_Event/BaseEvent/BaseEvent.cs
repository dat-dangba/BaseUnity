using System;
using System.Collections;
using UnityEngine;

public abstract class BaseEvent : BaseMonoBehaviour
{
    private readonly DateTime timeStart = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    [SerializeField]
    private bool isEventHappening;
    [SerializeField]
    private bool isInEvent;
    [SerializeField]
    private double eventTime;
    [SerializeField]
    private double nextEventTime;
    [SerializeField]
    private int numberOfEvents;

    protected double EventTime { get => eventTime; private set => eventTime = value; }
    protected double NextEventTime { get => nextEventTime; private set => nextEventTime = value; }
    protected bool IsEventHappening { get => isEventHappening; private set => isEventHappening = value; }
    public bool IsInEvent { get => isInEvent; private set => isInEvent = value; }

    public virtual void CheckEventStatus()
    {
        DateTime startDateTime = GetTimeStart();
        DateTime currentDateTime = TimeManager.Instance.DateTimeOffset.LocalDateTime;
        TimeSpan timeElapsed = currentDateTime - startDateTime;

        numberOfEvents = (int)(timeElapsed.TotalHours / (EventDuration() + BreakTime()).TotalHours);
        if ((GetLoop() != -1 && numberOfEvents >= GetLoop()) || timeElapsed.TotalHours < 0)
        {
            isInEvent = false;
            isEventHappening = false;
            eventTime = 0;
            nextEventTime = 0;
            return;
        }

        isInEvent = true;
        isEventHappening = (timeElapsed.TotalHours % (EventDuration() + BreakTime()).TotalHours) < EventDuration().TotalHours;

        DateTime eventStartTime = startDateTime + TimeSpan.FromHours(Math.Floor(timeElapsed.TotalHours / (EventDuration() + BreakTime()).TotalHours) * (EventDuration() + BreakTime()).TotalHours);
        DateTime eventEndTime = eventStartTime + EventDuration();
        eventTime = (float)(eventEndTime - currentDateTime).TotalSeconds;

        nextEventTime = (float)((EventDuration() + BreakTime()).TotalSeconds - (timeElapsed.TotalSeconds % (EventDuration() + BreakTime()).TotalSeconds));

        StopAllCoroutines();
        StartCoroutine(CountDownRemainingTime());
        StartCoroutine(CountDownTimeToNextEvent());

        //if (isEventHappening)
        //{
        //    DateTime eventStartTime = startDateTime + TimeSpan.FromHours(Math.Floor(timeElapsed.TotalHours / (EventDuration() + BreakTime()).TotalHours) * (EventDuration() + BreakTime()).TotalHours);
        //    DateTime eventEndTime = eventStartTime + EventDuration();
        //    eventTime = (float)(eventEndTime - currentDateTime).TotalSeconds;
        //    StopAllCoroutines();
        //    StartCoroutine(CountDownRemainingTime());
        //}
        //else
        //{
        //    nextEventTime = (float)((EventDuration() + BreakTime()).TotalSeconds - (timeElapsed.TotalSeconds % (EventDuration() + BreakTime()).TotalSeconds));
        //    StopAllCoroutines();
        //    StartCoroutine(CountDownTimeToNextEvent());
        //}
    }

    private IEnumerator CountDownTimeToNextEvent()
    {
        while (nextEventTime > 0)
        {
            nextEventTime -= Time.deltaTime;
            yield return null;
        }
        CheckEventStatus();
    }


    protected IEnumerator CountDownRemainingTime()
    {
        while (eventTime > 0)
        {
            eventTime -= Time.deltaTime;
            yield return null;
        }
        CheckEventStatus();
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

    public abstract void NextEvent();
}
