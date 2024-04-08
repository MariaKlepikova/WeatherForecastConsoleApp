using System.Configuration;
using System.Data;
using System.Windows;
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

		var currentWeatherViewModel = new CurrentWeatherViewModel();

		currentWeatherViewModel.Initialize();

		new MainWindow()
		{
			DataContext = currentWeatherViewModel,
		}.Show();
	}
}