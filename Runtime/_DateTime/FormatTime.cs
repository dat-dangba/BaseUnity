using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormatTime
{
    public static string Format(double totalSeconds)
    {
        int secondsInDay = 86400; // Số giây trong 1 ngày

        if (totalSeconds >= secondsInDay)
        {
            int days = (int)(totalSeconds / secondsInDay);
            TimeSpan time = TimeSpan.FromSeconds(totalSeconds - secondsInDay * days);

            if (time.Hours > 0)
            {
                return string.Format("{0:D1}d{2:D2}h", days, time.Hours, time.Hours);
            }
            else if (time.Minutes > 0)
            {
                return string.Format("{0:D1}d{2:D2}m", days, time.Hours, time.Minutes);
            }
            else
            {
                return string.Format("{0:D1}d{2:D2}s", days, time.Hours, time.Seconds);
            }
        }
        else
        {
            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
            return string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
        }
    }

    public static string FormatTimePlay(double totalSeconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
        return string.Format("{0:D1}:{1:D2}", time.Minutes, time.Seconds);
    }
}
