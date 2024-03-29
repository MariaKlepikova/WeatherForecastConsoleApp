using System.Text.Json.Serialization;
namespace WeatherForecastLibrary.Models;

public class DailyWeatherResponse
{
    [JsonPropertyName("daily")]
    public DailyForecast DailyForecast { get; set; }
}
public class DailyForecast
{
    [JsonPropertyName("time")]
    public DateTimeOffset[] Time { get; set; }
    
    [JsonPropertyName("temperature_2m_max")]
    public double[] MaxTemperature { get; set; }

    [JsonPropertyName("temperature_2m_min")]
    public double[] MinTemperature { get; set; }

    [JsonPropertyName("sunrise")]
    public string[] Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    public string[] Sunset { get; set; }
    
    [JsonPropertyName("precipitation_probability_max")]
    public double[] PrecipitationProbability { get; set; }

    [JsonPropertyName("wind_speed_10m_max")]
    public double[] WindSpeed { get; set; }
}