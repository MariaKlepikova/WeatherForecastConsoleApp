using System.Text.Json.Serialization;
namespace WeatherForecastLibrary.Models;

public class HourlyWeatherResponse
{
    [JsonPropertyName("hourly")]
    public HourlyForecast HourlyForecast { get; set; }
}
public class HourlyForecast
{
    [JsonPropertyName("time")]
    public string[] Time { get; set; }

    [JsonPropertyName("temperature_2m")]
    public double[] Temperature { get; set; }

    [JsonPropertyName("relative_humidity_2m")]
    public int[] RelativeHumidity { get; set; }

    [JsonPropertyName("wind_speed_10m")]
    public double[] WindSpeed { get; set; }
}