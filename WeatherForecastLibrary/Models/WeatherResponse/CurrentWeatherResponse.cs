using System.Text.Json.Serialization;
namespace WeatherForecastLibrary.Models;

    public class CurrentWeatherResponse
    {
        [JsonPropertyName("current")]
        public CurrentForecast CurrentForecast { get; set; }
    }

    public class CurrentForecast
    {
        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("temperature_2m")]
        public double Temperature { get; set; }

        [JsonPropertyName("relative_humidity_2m")]
        public int RelativeHumidity { get; set; } //относительная влажность

        [JsonPropertyName("wind_speed_10m")]
        public double WindSpeed { get; set; }
    }

    

    



