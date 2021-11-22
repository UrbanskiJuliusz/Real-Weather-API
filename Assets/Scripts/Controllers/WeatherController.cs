using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    private const string API_KEY = "API_KEY";
    [SerializeField] private LocationController locationController;
    public Weather Weather { get; private set; }

    private void OnEnable() { locationController.OnGetCityFromApi += GetActualWeather; }

    private void OnDisable() { locationController.OnGetCityFromApi -= GetActualWeather; }

    private void Awake()
    {
        Weather = new Weather();
    }

    private void GetActualWeather()
    {
        string getWeatherUrl = "https://api.openweathermap.org/data/2.5/weather?q=";
        getWeatherUrl += locationController.Location.city;
        getWeatherUrl += "&appid=" + API_KEY;

        StartCoroutine(GetDataFromApi(getWeatherUrl));
    }

    private IEnumerator GetDataFromApi(string getWeatherUrl)
    {
        Debug.Log(getWeatherUrl);
        
        yield return StartCoroutine(ApiHelper.Instance.HttpGet(getWeatherUrl));

        Weather = JsonUtility.FromJson<Weather>(ApiHelper.Instance.dataFromApi);

        if (Weather.weather.Count > 0)
            Debug.Log(Weather.weather[0].main);
    }
}
