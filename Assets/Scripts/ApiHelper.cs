using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApiHelper : MonoBehaviour
{
    public static ApiHelper Instance { get; private set; }
    public string dataFromApi;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public IEnumerator HttpGet(string url)
    {
        using UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError)
            Debug.Log($"Status: { unityWebRequest.responseCode } Error: { unityWebRequest.error }");

        if (unityWebRequest.isDone)
        {
            dataFromApi = unityWebRequest.downloadHandler.text;
        }
    }

}
