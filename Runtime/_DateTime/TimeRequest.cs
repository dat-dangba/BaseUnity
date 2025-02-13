using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeRequest : MonoBehaviour
{
    private int countRequest;
    private bool isRequested;

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
        if (isRequested)
        {
            return;
        }
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
        if (isRequested)
        {
            return;
        }
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
        catch (Exception)
        {
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
        isRequested = true;
        TimeManager.Instance.Init(TimeManager.Instance.GetTotalSeconds(dateTime));
        OnTimeRequestSuccess?.Invoke();
    }
}