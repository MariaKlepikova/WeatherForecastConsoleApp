using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WeatherForecastLibrary;
using WeatherForecastLibrary.Models;
using WeatherForecastLibrary.Services;
using WeatherWpfApp.Commands;
using WeatherWpfApp.Commands.TaskUtilities;

namespace WeatherWpfApp.ViewModels
{
	public class CurrentWeatherViewModel : INotifyPropertyChanged
	{
		/*
		 *   1. беру инпут значения из пропертей вьюмодели
		 *      (значения туда попадет, потому что к этой проперти прибинжен элемент управления на вью, в которое введет данные пользователь)
		 *   2. Формирую модель запроса в свой сервис-библиотеку
		 *   3. Вызываю метод из библиотеки и получаю ответ
		 *   4. Из полученного ответа заполняю проперти вьюмодели, чтобы значения отобразились на вью
		 */

		private readonly CitySearchService _citySearchService;
		private readonly Dictionary<Precipitation, WeatherType> _weatherIcons;

		public CurrentWeatherViewModel(CitySearchService citySearchService)
		{
			_citySearchService = citySearchService;

			SearchCitiesCommand = new AsyncCommand(
				OnCommandExecuteSearchCities,
				() => true);

			_weatherIcons = new Dictionary<Precipitation, WeatherType>()
			{
				{
					Precipitation.Clearly, new WeatherType()
					{
						IconSource =
							new BitmapImage(new Uri("D:\\Машулька\\Downloads\\mycollection\\png\\001-sunny.png",
								UriKind.Absolute)),
						TextDescription = "ясно"
					}
				},

				{
					Precipitation.Cloudy, new WeatherType()
					{
						IconSource =
							new BitmapImage(new Uri("D:\\Машулька\\Downloads\\mycollection\\png\\002-cloudy.png",
								UriKind.Absolute)),
						TextDescription = "облачно"
					}
				},

				{
					Precipitation.PartlyCloudy, new WeatherType()
					{
						IconSource =
							new BitmapImage(new Uri("D:\\Машулька\\Downloads\\mycollection\\png\\003-cloudy-1.png",
								UriKind.Absolute)),
						TextDescription = "облачно с прояснениями"
					}
				},

				{
					Precipitation.Rain, new WeatherType()
					{
						IconSource =
							new BitmapImage(new Uri("D:\\Машулька\\Downloads\\mycollection\\png\\004-rainy-day.png",
								UriKind.Absolute)),
						TextDescription = "небольшой дождь"
					}
				},

				{
					Precipitation.Shower, new WeatherType()
					{
						IconSource =
							new BitmapImage(new Uri("D:\\Машулька\\Downloads\\mycollection\\png\\005-rainy-day-1.png",
								UriKind.Absolute)),
						TextDescription = "ливень"
					}
				},

				{
					Precipitation.Snow, new WeatherType()
					{
						IconSource = new BitmapImage(new Uri("D:\\Машулька\\Downloads\\mycollection\\png\\006-snow.png",
							UriKind.Absolute)),
						TextDescription = "снег"
					}
				},
			};
		}

		private ImageSource _weatherIcon;

		public ImageSource WeatherIcon
		{
			get => _weatherIcon;
			set
			{
				_weatherIcon = value;
				OnPropertyChanged(nameof(WeatherIcon));
			}
		}

		private string _textDescription;

		public string TextDescription
		{
			get => _textDescription;
			set
			{
				_textDescription = value;
				OnPropertyChanged(nameof(TextDescription));
			}
		}

		private string _nameOfCity;

		public string NameOfCity
		{
			get { return _nameOfCity; }
			set
			{
				_nameOfCity = value;
				OnPropertyChanged(nameof(NameOfCity));
			}
		}

		private string _date;

		public string Date
		{
			get { return _date; }
			set
			{
				_date = value;

				OnPropertyChanged(nameof(Date));
			}
		}

		private string _temperature;

		public string Temperature
		{
			get { return _temperature; }
			set
			{
				_temperature = value;
				OnPropertyChanged(nameof(Temperature));
			}
		}

		private string _precipitationProbability;

		public string PrecipitationProbability
		{
			get { return _precipitationProbability; }
			set
			{
				_precipitationProbability = value;
				OnPropertyChanged(nameof(PrecipitationProbability));
			}
		}

		private string _maxTemperature;

		public string MaxTemperature
		{
			get { return _maxTemperature; }
			set
			{
				_maxTemperature = value;
				OnPropertyChanged(nameof(MaxTemperature));
			}
		}

		private string _minTemperature;

		public string MinTemperature
		{
			get { return _minTemperature; }
			set
			{
				_minTemperature = value;
				OnPropertyChanged(nameof(MinTemperature));
			}
		}

		private string _windSpeed;

		public string WindSpeed
		{
			get { return _windSpeed; }
			set
			{
				_windSpeed = value;
				OnPropertyChanged(nameof(WindSpeed));
			}
		}

		private string _relativeHumidity;

		public string RelativeHumidity
		{
			get { return _relativeHumidity; }
			set
			{
				_relativeHumidity = value;
				OnPropertyChanged(nameof(RelativeHumidity));
			}
		}

		private string _sunrise;

		public string Sunrise
		{
			get { return _sunrise; }
			set
			{
				_sunrise = value;
				OnPropertyChanged(nameof(Sunrise));
			}
		}

		private string _sunset;

		public string Sunset
		{
			get { return _sunset; }
			set
			{
				_sunset = value;
				OnPropertyChanged(nameof(Sunset));
			}
		}

		private string _cityNameInput;

		public string CityNameInput
		{
			get { return _cityNameInput; }
			set
			{
				_cityNameInput = value;
				OnPropertyChanged(nameof(CityNameInput));
			}
		}


		private CitySearchResponse[] CurrentCitiesCache { get; set; }

		private string[] _availableCitiesForDropdown;

		public string[] AvailableCitiesForDropdownForDropdown
		{
			get { return _availableCitiesForDropdown; }
			set
			{
				_availableCitiesForDropdown = value;
				OnPropertyChanged(nameof(AvailableCitiesForDropdownForDropdown));
			}
		}

		private string _selectedCityFromDropdown;

		public string SelectedCityFromDropdown
		{
			get { return _selectedCityFromDropdown; }
			set
			{
				//CityNameInput = value;

				if (value != null)
				{
					_ = ChangeWeatherForecastByCityName(value);
				}

				_selectedCityFromDropdown = value;
				OnPropertyChanged(nameof(SelectedCityFromDropdown));
			}
		}

		private async Task ChangeWeatherForecastByCityName(string selectedCityName)
		{
			var selectedCity = CurrentCitiesCache.FirstOrDefault(city => city.DisplayName == selectedCityName);

			NameOfCity = selectedCity.Name;

			var valueLatitude = selectedCity.Latitude;
			var valueLongitude = selectedCity.Longitude;

			InitializeProperties(valueLatitude, valueLongitude);
		}

		public async void Initialize()
		{
			NameOfCity = "Санкт-Петербург";

			Date = DateTime.Now.ToString("d MMMM, dddd");

			var valueLatitude = "59.9386";
			var valueLongitude = "30.3141";

			InitializeProperties(valueLatitude, valueLongitude);
		}

		private async void InitializeProperties(string valueLatitude, string valueLongitude) 
		{
			var weatherApiClient = new WeatherApiClient();

			var currentWeather = await weatherApiClient.GetCurrentForecast(valueLatitude, valueLongitude);
			var dailyWeather = await weatherApiClient.GetDailyForecast(valueLatitude, valueLongitude);

			Temperature = currentWeather.CurrentForecast.Temperature.ToString("#0 '°C'");

			PrecipitationProbability = dailyWeather.DailyForecast.PrecipitationProbability[0].ToString("#0");

			MaxTemperature = dailyWeather.DailyForecast.MaxTemperature[0].ToString("#0 '°C'");

			MinTemperature = dailyWeather.DailyForecast.MinTemperature[0].ToString("#0 '°C'");

			WindSpeed = currentWeather.CurrentForecast.WindSpeed.ToString("##,# м/с");

			RelativeHumidity = currentWeather.CurrentForecast.RelativeHumidity.ToString("##");

			Sunrise = dailyWeather.DailyForecast.Sunrise[0].ToString("t");

			Sunset = dailyWeather.DailyForecast.Sunset[0].ToString("t");

			var typeOfPrecipitation = weatherApiClient.TranscribeStateOfPrecipitation(currentWeather);

			var currentWeatherType = _weatherIcons[typeOfPrecipitation];

			WeatherIcon = currentWeatherType.IconSource;

			TextDescription = currentWeatherType.TextDescription;
		}

		public IAsyncCommand SearchCitiesCommand { get; }

		private async Task OnCommandExecuteSearchCities()
		{
			var userInput = CityNameInput;

			CurrentCitiesCache = await _citySearchService.ShowCitiesForOutput(userInput);

			AvailableCitiesForDropdownForDropdown = CurrentCitiesCache.Select(names => names.DisplayName).ToArray();

			SelectedCityFromDropdown = AvailableCitiesForDropdownForDropdown.FirstOrDefault();
		}

		public event PropertyChangedEventHandler? PropertyChanged; 
		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
		{ 
			if (EqualityComparer<T>.Default.Equals(field, value)) return false; 
			field = value; 
			OnPropertyChanged(propertyName); 
			return true;
		}
	}
}
