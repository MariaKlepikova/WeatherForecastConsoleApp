using System.Text.Json;
using System.Text.Json.Serialization;
using WeatherForecastLibrary.Models;

namespace WeatherForecastLibrary;

public class WeatherApiClient
{
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast")
    };

    public async Task<CurrentWeatherResponse> GetCurrentForecast(string valueLatitude, string valueLongitude)
    {
        var urlForCurrentForecast =
            $"?latitude={valueLatitude}&longitude={valueLongitude}&current=temperature_2m,relative_humidity_2m,rain,snowfall,cloud_cover,wind_speed_10m&wind_speed_unit=ms&timezone=Europe%2FMoscow";

		var response = await _httpClient.GetAsync(urlForCurrentForecast);

        var userResponse = await response.Content.ReadAsStringAsync();  

        return JsonSerializer.Deserialize<CurrentWeatherResponse>(userResponse);
    }

    public async Task<DailyWeatherResponse> GetDailyForecast(string valueLatitude, string valueLongitude)
    {
        var urlForDailyForecast =
            $"?latitude={valueLatitude}&longitude={valueLongitude}&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,precipitation_probability_max,wind_speed_10m_max&wind_speed_unit=ms&timezone=Europe%2FMoscow";

        var response = await _httpClient.GetAsync(urlForDailyForecast);

        var userResponse = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<DailyWeatherResponse>(userResponse);
    }

    public async Task<HourlyWeatherResponse> GetHourlyForecast(string valueLatitude, string valueLongitude)
    {
        var urlForHourlyForecast =
            $"?latitude={valueLatitude}&longitude={valueLongitude}&hourly=temperature_2m,relative_humidity_2m,wind_speed_10m&wind_speed_unit=ms&timezone=Europe%2FMoscow&forecast_days=1";

        var response = await _httpClient.GetAsync(urlForHourlyForecast);

        var userResponse = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<HourlyWeatherResponse>(userResponse);
    }

    public Precipitation TranscribeStateOfPrecipitation(CurrentWeatherResponse currentWeather)
    {
	    var rain = currentWeather.CurrentForecast.Rain;
	    var cloudy = currentWeather.CurrentForecast.CloudCover;
	    var snow = currentWeather.CurrentForecast.Snowfall;

	    if (rain < 20 && cloudy < 10 && snow <= 0)
	    { return Precipitation.Clearly; }

        if (rain >= 20 && rain < 50 && snow <= 0)
        { return Precipitation.Rain; }

        if (rain >= 50 && snow <= 0)
        { return Precipitation.Shower; }

        if (rain < 20 && cloudy >= 10 && cloudy < 80 && snow <= 0)
        { return Precipitation.PartlyCloudy; }

        if (rain < 20 && cloudy >= 80 && snow <= 0)
        { return Precipitation.Cloudy; }

        return Precipitation.Snow;
    }
}