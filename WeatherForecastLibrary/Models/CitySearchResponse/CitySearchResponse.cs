using System.Text.Json.Serialization;
namespace WeatherForecastLibrary.Models;

public class CitySearchResponse
{
    [JsonPropertyName("lat")]
    public string Latitude { get; set; }

    [JsonPropertyName("lon")]
    public string Longitude { get; set; } 
    
    [JsonPropertyName("addresstype")]
    public string AdressType { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; }
}