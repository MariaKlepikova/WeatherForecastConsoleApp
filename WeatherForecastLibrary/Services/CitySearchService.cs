using System.Text.Json;
using System.Xml.Linq;
using WeatherForecastLibrary.Models;

namespace WeatherForecastLibrary.Services;

public class CitySearchService
{
    private readonly HttpClient _httpClient = new();

    private async Task<CitySearchResponse[]> SearchCities(string userInput)
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

    public async Task<CitySearchResponse[]> ShowCitiesForOutput(string userInput)  
    {
        var allCities = await SearchCities(userInput);

        return allCities.Where(city => city.AdressType is "city" or "town" or "village").ToArray();
	}
}

    
