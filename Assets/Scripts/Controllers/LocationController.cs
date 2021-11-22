using System.Collections;
using UnityEngine;
using System;

public class LocationController : MonoBehaviour
{
    private string IPUrl = "https://api.ipify.org/?format=json";
    private string locationUrl = "http://ip-api.com/json/";
    private AddressIP addressIP = new AddressIP();
    public Location Location { get; private set; } = new Location();
    public Action OnGetCityFromApi = delegate { };

    private IEnumerator Start()
    {
        yield return StartCoroutine(GetIPAddress());
        locationUrl += addressIP.ip + "?fields=status,city";

        yield return StartCoroutine(GetUserCountry());
        if (Location.status == "fail")
            Debug.Log("Check your internet connection and Try again");
        else
            OnGetCityFromApi();
    }

    private IEnumerator GetIPAddress()
    {
        yield return StartCoroutine(ApiHelper.Instance.HttpGet(IPUrl));
        addressIP = JsonUtility.FromJson<AddressIP>(ApiHelper.Instance.dataFromApi);
    }

    private IEnumerator GetUserCountry()
    {
        yield return StartCoroutine(ApiHelper.Instance.HttpGet(locationUrl));
        Location = JsonUtility.FromJson<Location>(ApiHelper.Instance.dataFromApi);
    }
}
