using alkan_task.Helpers;
using alkan_task.ViewModels;
using CountryData.Standard;
using Newtonsoft.Json.Linq;
using System.Windows.Input;

namespace alkan_task.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
		BindingContext = App.viewModel;
	}

    


    private async void Country_Selected(object sender, EventArgs e)
    {
		var SelectedCountry = countryPicker.SelectedItem as Country;
		Console.WriteLine(SelectedCountry.CountryName);
		var data = await API_Helper.Search_Query(SelectedCountry.CountryName);
		if (data == null) return;
		var jsonObject = JObject.Parse(data);
        var city = jsonObject["location"]["name"].ToString();
        var temp = jsonObject["current"]["temp_c"].ToString();
		var humidity = jsonObject["current"]["humidity"].ToString();
		var windspeed = jsonObject["current"]["wind_kph"].ToString();
		var weathercondition = jsonObject["current"]["condition"]["text"].ToString();
        App.viewModel.set_countryData(city,temp,humidity,windspeed,weathercondition);
    }

    private void City_Forecast(object sender, EventArgs e)
    {
		Navigation.PushAsync(new ForecastPage());
    }
}