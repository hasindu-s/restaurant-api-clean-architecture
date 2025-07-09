﻿namespace Restaurants.API.Controllers;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> Get();
    IEnumerable<WeatherForecast> Generate(int numOfResults, int min, int max);
}

public class WeatherForecastService : IWeatherForecastService
{
    private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    public IEnumerable<WeatherForecast> Generate(int numOfResults, int min, int max)
    {
        return Enumerable.Range(1, numOfResults).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(min, max),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
