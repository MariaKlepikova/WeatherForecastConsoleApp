using WeatherForecastLibrary.Models;

namespace WeatherForecastLibrary.Repositories;

public class CitiesRepository : ICitiesRepository
{
    private Dictionary<string, City> _cities = new()
    {
        {
            "Санкт-Петербург", new City
            {
                Name = "Санкт-Петербург",
                Сoordinates = new Сoordinates
                {
                    Latitude = "59.9386",
                    Longitude = "30.3141"
                }
            }
        },
        {
            "Москва", new City
            {
                Name = "Москва",
                Сoordinates = new Сoordinates
                {
                    Latitude = "55.7522",
                    Longitude = "37.6156"
                }
            }
        },
        {
            "Ереван", new City
            {
                Name = "Ереван",
                Сoordinates = new Сoordinates
                {
                    Latitude = "40.1811",
                    Longitude = "44.5136"
                }
            }
        },
        {
            "Милан", new City
            {
                Name = "Милан",
                Сoordinates = new Сoordinates
                {
                    Latitude = "45.4643",
                    Longitude = "9.1895"
                }
            }
        },
        {
            "Выборг", new City
            {
                Name = "Выборг",
                Сoordinates = new Сoordinates
                {
                    Latitude = "60.7076",
                    Longitude = "28.7528"
                }
            }
        },
    };

    public string[] GetAllCities()
    {
        return _cities.Keys.ToArray();
    }

    public City? GetCoordinatesOfCity(string nameRequiredCity)
    {
        _cities.TryGetValue(nameRequiredCity, out City? coordinatesOfCity);
        
        return coordinatesOfCity;
    }
}