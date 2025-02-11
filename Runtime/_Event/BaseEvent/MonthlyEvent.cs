using System;

public abstract class MonthlyEvent : BaseEvent
{

    protected override TimeSpan BreakTime()
    {
        return TimeSpan.FromDays(0);
    }

    protected override TimeSpan EventDuration()
    {
        int year = TimeManager.Instance.DateTimeOffset.Year;
        int month = TimeManager.Instance.DateTimeOffset.Month;
        int daysInMonth = DateTime.DaysInMonth(year, month);
        return TimeSpan.FromDays(daysInMonth);
    }
}