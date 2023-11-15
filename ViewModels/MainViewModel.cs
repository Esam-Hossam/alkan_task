using alkan_task.Helpers;
using alkan_task.Models;
using CountryData.Standard;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace alkan_task.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            // Fetching countries for Picker List
            Countries = new ObservableCollection<Country>(new CountryHelper().GetCountryData());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
       PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public ObservableCollection<Country> Countries { get; set; }
        private ObservableCollection<WeatherModel> _forecast { get; set; }
        // Forecast List
        public ObservableCollection<WeatherModel> Forecast
        {
            get => _forecast;
            set
            {
                if (_forecast != value)
                {
                    _forecast = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _selectedCity { get; set; } = "";
        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                if (_selectedCity != value)
                {
                    _selectedCity = value;
                    OnPropertyChanged();
                }
            }
        }
        private WeatherModel _selectedCountry { get; set; }
        public WeatherModel SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                if (_selectedCountry != value)
                {
                    _selectedCountry = value;
                    OnPropertyChanged();
                }
            }
        }

        // Setting our viewmodel variables with the fetched online Data
        public void set_countryData(string city = "", string temp = "", string humidity = "", string windspeed = "", string weathercondition = "")
        {
            SelectedCity = city;
            SelectedCountry = new WeatherModel()
            {
                Temperature = temp,
                Humidity = humidity,
                WindSpeed = windspeed,
                WeatherCondition = weathercondition
            };
        }

       

        public ICommand SearchCityCommand => new Command<string>(async (string query) =>
        {
            var data = await API_Helper.Search_Query(query);
            if (data != null)
            {
                var jsonObject = JObject.Parse(data);
                if (jsonObject["error"] != null) {
                    App.Current.MainPage.DisplayAlert("Error", jsonObject["error"]["message"].ToString(),"OK");
                    return;
                }
                var city = jsonObject["location"]["name"].ToString();
                var temp = jsonObject["current"]["temp_c"].ToString();
                var humidity = jsonObject["current"]["humidity"].ToString();
                var windspeed = jsonObject["current"]["wind_kph"].ToString();
                var weathercondition = jsonObject["current"]["condition"]["text"].ToString();
                set_countryData(city, temp, humidity, windspeed, weathercondition);
            }
        });
    }
}
