using System.Text.Json;
using WeatherForecastLibrary.Models;

namespace WeatherForecastLibrary.Services;

public class CitySearchService
{
    private readonly HttpClient _httpClient = new();

    public async Task<CitySearchResponse[]> SearchCities(string userInput)
    {
        var request = new HttpRequestMessage()
        {
            RequestUri = new Uri(
                "https://nominatim.openstreetmap.org/search" +
                "?format=json&" +
                $"q={userInput}'"),
            Method = HttpMethod.Get
        };

        request.Headers.Add("User-Agent", "my-pretty-weather-application");
        request.Headers.Add("Accept-Language", "ru");

        var response = await _httpClient.SendAsync(request);

        var responseMessage = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CitySearchResponse[]>(responseMessage);
    }

    public string[] ShowCitiesForOutput(CitySearchResponse[] allCities)
    {
        CitySearchResponse[] availableCities = null;

        var groupOfCities = from CitySearchResponse in allCities group CitySearchResponse by CitySearchResponse.Type;

        foreach (var element in groupOfCities)
        {
            if (element.Key is "city" or "town")
            {
                availableCities = element.ToArray();
            }
        }
        return availableCities.Select(name => name.DisplayName).ToArray();
    }
}

    
