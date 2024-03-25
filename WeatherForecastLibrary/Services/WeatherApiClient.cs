using System.Text.Json;
using System.Text.Json.Serialization;
using WeatherForecastLibrary.Models;

namespace WeatherForecastLibrary;

public class WeatherApiClient
{
    private HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast")
    };

    public async Task<CurrentWeatherResponse> GetCurrentForecast(string valueLatitude, string valueLongitude)
    {
        var urlForCurrentForecast =
            $"?latitude={valueLatitude}&longitude={valueLongitude}&current=temperature_2m,relative_humidity_2m,wind_speed_10m&wind_speed_unit=ms&timezone=Europe%2FMoscow&forecast_days=1";

        var response = await httpClient.GetAsync(urlForCurrentForecast);

        var userResponse = await response.Content.ReadAsStringAsync();  

        return JsonSerializer.Deserialize<CurrentWeatherResponse>(userResponse);
    }

    public async Task<DailyWeatherResponse> GetDailyForecast(string valueLatitude, string valueLongitude)
    {
        var urlForDailyForecast =
            $"?latitude={valueLatitude}&longitude={valueLongitude}&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset&wind_speed_unit=ms&timezone=Europe%2FMoscow&forecast_days=1";

        var response = await httpClient.GetAsync(urlForDailyForecast);

        var userResponse = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<DailyWeatherResponse>(userResponse);
    }

    public async Task<HourlyWeatherResponse> GetHourlyForecast(string valueLatitude, string valueLongitude)
    {
        var urlForHourlyForecast =
            $"?latitude={valueLatitude}&longitude={valueLongitude}&hourly=temperature_2m,relative_humidity_2m,wind_speed_10m&wind_speed_unit=ms&timezone=Europe%2FMoscow&forecast_days=1";

        var response = await httpClient.GetAsync(urlForHourlyForecast);

        var userResponse = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<HourlyWeatherResponse>(userResponse);
    }
}