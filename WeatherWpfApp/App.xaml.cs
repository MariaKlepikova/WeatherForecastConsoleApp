using System.Configuration;
using System.Data;
using System.Windows;
using WeatherForecastLibrary.Services;
using WeatherWpfApp.ViewModels;

namespace WeatherWpfApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);

		var cityService = new CitySearchService();
		
		var currentWeatherViewModel = new CurrentWeatherViewModel(cityService);

		currentWeatherViewModel.Initialize();

		new MainWindow()
		{
			DataContext = currentWeatherViewModel,
		}.Show();
	}
}