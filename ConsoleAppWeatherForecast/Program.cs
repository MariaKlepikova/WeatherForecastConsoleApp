using WeatherForecastLibrary;
using WeatherForecastLibrary.Models;
using WeatherForecastLibrary.Repositories;

namespace ConsoleAppWeatherForecast
{
    internal class Program
    {
        private static readonly CitiesRepository CitiesRepository = new CitiesRepository();
        
        public static async Task Main(string[] args)
        {
            var availableForInputCities = CitiesRepository.GetAllCities();
            
            Console.WriteLine("ПРОГНОЗ ПОГОДЫ \n");
            
            Console.WriteLine("Прогноз погоды доступен в следующих городах: \n");

            foreach (var cityName in availableForInputCities)
            {
                Console.WriteLine(cityName);
            }
            
            Console.WriteLine("\n Введите название нужного города:");

            var cityInput = Console.ReadLine();

            while (availableForInputCities.Contains(cityInput) is false)
            {
                Console.WriteLine("Такого города нет, введите ещё раз внимательнее");
                cityInput = Console.ReadLine();
            }
            Console.Clear();

            var cityCoordinatesForWeather = CitiesRepository.GetCoordinatesOfCity(cityInput!);
            
            string valueLatitude = cityCoordinatesForWeather.Сoordinates.Latitude;
            
            string valueLongitude = cityCoordinatesForWeather.Сoordinates.Longitude;
            
            string nameOfCity = cityCoordinatesForWeather.Name;

            WeatherApiClient weatherApiClient = new WeatherApiClient();

            var currentForecast = await weatherApiClient.GetCurrentForecast(valueLatitude, valueLongitude);

            var dailyForecast = await weatherApiClient.GetDailyForecast(valueLatitude, valueLongitude);

            var hourlyForecast = await weatherApiClient.GetHourlyForecast(valueLatitude, valueLongitude);
            
            PrintCurrentForecast(currentForecast, nameOfCity);

            PrintDailyForecast(dailyForecast);

            PrintHourlyForecast(hourlyForecast);

            Console.ReadLine();
        }

        public static void PrintCurrentForecast(CurrentWeatherResponse currentForecast, string nameOfCity)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Текущая погода, {nameOfCity}, {currentForecast.CurrentForecast.Time}");
            Console.ResetColor(); 
            
            Console.WriteLine($"Температура воздуха: {currentForecast.CurrentForecast.Temperature} \u00b0C \n"
                              + $"Относительная влажность: {currentForecast.CurrentForecast.RelativeHumidity} % \n"
                              + $"Скорость ветра: {currentForecast.CurrentForecast.WindSpeed} м/с \n");
        }
        
        public static void PrintDailyForecast(DailyWeatherResponse dailyForecast)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Прогноз погоды на день:");
            Console.ResetColor(); 
            
            Console.WriteLine($"Максимальная температура воздуха: {dailyForecast.DailyForecast.MaxTemperature[0]} \u00b0C \n"
                              + $"Минимальная температура воздуха: {dailyForecast.DailyForecast.MinTemperature[0]} \u00b0C \n"
                              + $"Восход в: {dailyForecast.DailyForecast.Sunrise[0]} \n"
                              + $"Закат в: {dailyForecast.DailyForecast.Sunset[0]} \n");
        }
        
        public static void PrintHourlyForecast(HourlyWeatherResponse hourlyForecast)
        { 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Прогноз погоды по часам:");
            Console.ResetColor(); 
            
            int length = hourlyForecast.HourlyForecast.Time.Length;

            for (int index = 0; index < length; index++)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"{hourlyForecast.HourlyForecast.Time[index]}:");
                Console.ResetColor(); 
                
                Console.WriteLine($"\t Температура воздуха: {hourlyForecast.HourlyForecast.Temperature[index]} \u00b0C\n"
                                  + $"\t Относительняа влажность: {hourlyForecast.HourlyForecast.RelativeHumidity[index]} %\n"
                                  + $"\t Скорость ветра: {hourlyForecast.HourlyForecast.WindSpeed[index]} м/с\n");
            }
        }
    }
}






