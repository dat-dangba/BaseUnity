using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class BaseNetworkManager<I> : BaseMonoBehaviour where I : MonoBehaviour
{
    public bool IsConnected { get; private set; }

    private bool isRequesting;
    private bool isFirstNetworkChanged;

    private readonly int timeout = 3; // Thời gian timeout (giây)
    private readonly float checkInterval = 3f; // Khoảng thời gian kiểm tra kết nối (giây)

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

    public void StartCheckConnection()
    {
        StartCoroutine(CheckInternetConnection());

        if (IsAutoCheckNetwork())
        {
            StartCoroutine(CheckInternetPeriodically());
        }
    }

    public void CheckConnection()
    {
        StartCoroutine(CheckInternetConnection());
    }

    private IEnumerator CheckInternetPeriodically()
    {
        while (true)
        {
            if (CanAutoCheckConnection())
            {
                yield return CheckInternetConnection();
            }
            yield return new WaitForSeconds(GetCheckInterval());
        }
    }

    public bool IsNetworkAvailable()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }

    private string GetCheckUrl()
    {
#if UNITY_ANDROID
        return "https://connectivitycheck.gstatic.com/generate_204";
#elif UNITY_IOS
        return "https://captive.apple.com/hotspot-detect.html";
#else
        return "https://clients3.google.com/generate_204";
#endif
    }

    private IEnumerator CheckInternetConnection()
    {
        if (isRequesting)
        {
            yield break;
        }

        isRequesting = true;

        if (!IsNetworkAvailable())
        {
            NetworkStateChanged(false);
            yield break;
        }

        string url = GetCheckUrl();
        using UnityWebRequest request = UnityWebRequest.Head(url);
        request.timeout = GetTimeOutRequest();
        yield return request.SendWebRequest();

        NetworkStateChanged(!(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError));
    }

    protected virtual void NetworkStateChanged(bool isConnected)
    {
        if (isConnected != IsConnected || !isFirstNetworkChanged)
        {
            isFirstNetworkChanged = true;
            IsConnected = isConnected;
            OnNetworkStateChanged(IsConnected);
        }
        isRequesting = false;
    }

    protected virtual bool IsAutoCheckNetwork()
    {
        return true;
    }

    protected virtual bool CanAutoCheckConnection()
    {
        return true;
    }

    protected virtual float GetCheckInterval()
    {
        return checkInterval;
    }

    protected virtual int GetTimeOutRequest()
    {
        return timeout;
    }

    protected abstract void OnNetworkStateChanged(bool isConnected);
}
