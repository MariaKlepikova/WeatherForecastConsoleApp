
namespace WeatherForecastLibrary.Models;

public class City
{
    public string Name { get; init; }
    
    public Сoordinates Сoordinates { get; init; }
}

public class Сoordinates
{
    public string Latitude { get; init; }

    public string Longitude { get; init; }
}
