using WeatherForecastLibrary.Models;

namespace WeatherForecastLibrary.Repositories;

public interface ICitiesRepository
{
    public string[] GetAllCities();

    public City? GetCoordinatesOfCity(string nameRequiredCity);
}