using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeRequest : MonoBehaviour
{
    private int countRequest;

    private readonly List<string> ntps = new()
    {
        "pool.ntp.org",
        "time.google.com",
        "time.cloudflare.com",
        "time.windows.com",
    };

    public static event Action OnTimeRequestSuccess;

    public void Request()
    {
        countRequest = 0;
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            RequestNetWorkTime();
        }
        else
        {
            InitTime(DateTime.UtcNow);
        }
    }

    private void RequestNetWorkTime()
    {
        try
        {
            NtpClient client = new(ntps[countRequest]);
            using (client)
            {
                //Get Time Online
                DateTime dt = client.GetNetworkTime();
                InitTime(dt);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"datdb - {e.Message}");
            countRequest++;
            if (countRequest >= ntps.Count)
            {
                //Get Time Offline
                InitTime(DateTime.UtcNow);
            }
            else
            {
                Invoke(nameof(RequestNetWorkTime), 1);
            }
        }
    }

    private void InitTime(DateTime dateTime)
    {
        Debug.Log($"datdb - InitTime {dateTime.ToLongDateString()} {dateTime.ToLongTimeString()}");
        TimeManager.Instance.Init(TimeManager.Instance.GetTotalSeconds(dateTime));
        OnTimeRequestSuccess?.Invoke();
    }
}