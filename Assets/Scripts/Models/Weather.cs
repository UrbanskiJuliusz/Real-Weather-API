using System;
using System.Collections.Generic;

[Serializable]
public class Weather
{
    public List<MainWeather> weather;
}

[Serializable]
public class MainWeather
{
    public string main;
    public string description;
}