using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WeatherForecastLibrary;
using WeatherForecastLibrary.Models;
using WeatherWpfApp.Commands;

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

		private readonly Dictionary<Precipitation, WeatherType> _weatherIcons; 

		public CurrentWeatherViewModel()
		{
			ClickCommand = new LambdaCommand(
				new Action<object>(param => OnClickExecuted(param)),
				new Func<object, bool>(param => CanClickExecute(param))
			);

			_weatherIcons = new Dictionary<Precipitation, WeatherType>()
			{
				{ 
					Precipitation.Clearly, new WeatherType()
					{
						IconSource = new BitmapImage(new Uri("D:\\Машулька\\Downloads\\mycollection\\png\\001-sunny.png", UriKind.Absolute)),
						TextDescription = "ясно"
					}
				},

				{
					Precipitation.Cloudy, new WeatherType()
					{
						IconSource = new BitmapImage(new Uri("D:\\Машулька\\Downloads\\mycollection\\png\\002-cloudy.png", UriKind.Absolute)),
						TextDescription = "облачно"
					}
				},

				{
					Precipitation.PartlyCloudy, new WeatherType()
					{
						IconSource = new BitmapImage(new Uri("D:\\Машулька\\Downloads\\mycollection\\png\\003-cloudy-1.png", UriKind.Absolute)),
						TextDescription = "облачно с прояснениями"
					}
				},

				{
					Precipitation.Rain, new WeatherType()
					{
						IconSource = new BitmapImage(new Uri("D:\\Машулька\\Downloads\\mycollection\\png\\004-rainy-day.png", UriKind.Absolute)),
						TextDescription = "небольшой дождь"
					}
				},

				{
					Precipitation.Shower, new WeatherType()
					{
						IconSource = new BitmapImage(new Uri("D:\\Машулька\\Downloads\\mycollection\\png\\005-rainy-day-1.png", UriKind.Absolute)),
						TextDescription = "ливень"
					}
				},

				{
					Precipitation.Snow, new WeatherType()
					{
						IconSource = new BitmapImage(new Uri("D:\\Машулька\\Downloads\\mycollection\\png\\006-snow.png", UriKind.Absolute)),
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

		private string _clickContent;
		public string ClickContent
		{
			get { return _clickContent; }
			set
			{
				_clickContent = value;
				OnPropertyChanged(nameof(ClickContent));
			}
		}

		public ICommand ClickCommand { get; }


		private Random Rnd { get; } = new Random();
		private void OnClickExecuted(object sender)
		{
			ClickContent = Rnd.Next().ToString();
		}

		private bool CanClickExecute(object sender) => true;

		public async void Initialize()
		{
			NameOfCity = "Санкт-Петербург";

			Date = DateTime.Now.ToString("d MMMM, dddd");

			var valueLatitude = "59.9386";
			var valueLongitude = "30.3141";

			var weatherApiClient = new WeatherApiClient();

			var currentWeather = await weatherApiClient.GetCurrentForecast(valueLatitude, valueLongitude);
			var dailyWeather = await weatherApiClient.GetDailyForecast(valueLatitude, valueLongitude);

			Temperature = currentWeather.CurrentForecast.Temperature.ToString("#0 '°C'");

			PrecipitationProbability = dailyWeather.DailyForecast.PrecipitationProbability[0].ToString("##");

			MaxTemperature = dailyWeather.DailyForecast.MaxTemperature[0].ToString("## '°C'");

			MinTemperature = dailyWeather.DailyForecast.MinTemperature[0].ToString("## '°C'");

			WindSpeed = currentWeather.CurrentForecast.WindSpeed.ToString("##,# м/с");

			RelativeHumidity = currentWeather.CurrentForecast.RelativeHumidity.ToString("##");

			Sunrise = dailyWeather.DailyForecast.Sunrise[0].ToString("t");

			Sunset = dailyWeather.DailyForecast.Sunset[0].ToString("t");

			var typeOfPrecipitation = weatherApiClient.TranscribeStateOfPrecipitation(currentWeather);

			var currentWeatherType = _weatherIcons[typeOfPrecipitation];

			WeatherIcon = currentWeatherType.IconSource;

			TextDescription = currentWeatherType.TextDescription;
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
