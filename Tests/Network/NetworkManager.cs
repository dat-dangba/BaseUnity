using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : BaseNetworkManager<NetworkManager>
{
    [SerializeField]
    private GameObject noInternetUI;

    protected override void Start()
    {
        base.Start();
        //test
        StartCheckConnection();
    }

    protected override void OnNetworkStateChanged(bool isConnected)
    {
        Debug.Log($"datdb - OnNetworkStateChanged {isConnected}");
        noInternetUI.SetActive(!isConnected);
    }
}
