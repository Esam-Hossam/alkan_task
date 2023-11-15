using alkan_task.Helpers;
using alkan_task.Models;
using alkan_task.ViewModels;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace alkan_task.Views;

public partial class ForecastPage : ContentPage
{
    public ForecastPage()
	{
		InitializeComponent();
		BindingContext = App.viewModel;
		this.Title = App.viewModel.SelectedCity + " - Forecast";
		Task.Run(get_Forecast);
	}

	private async Task get_Forecast()
	{
		if (string.IsNullOrEmpty(App.viewModel.SelectedCity))
			return;
		var data = await API_Helper.get_CityForecast(App.viewModel.SelectedCity);
		if (data == null)
			return;
		App.viewModel.Forecast = new ObservableCollection<WeatherModel>();
		var jsonObject = JObject.Parse(data);
		foreach (var day in jsonObject["forecast"]["forecastday"])
		{
			App.viewModel.Forecast.Add(new WeatherModel()
			{
				Date = day["date"].ToString(),
				Temperature = day["day"]["avgtemp_c"].ToString(),
				Humidity = day["day"]["avghumidity"].ToString(),
				WindSpeed = day["day"]["maxwind_kph"].ToString(),
				WeatherCondition = day["day"]["condition"]["text"].ToString()
			});
		}

	}
}