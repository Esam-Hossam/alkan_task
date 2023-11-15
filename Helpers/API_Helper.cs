using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alkan_task.Helpers
{
    public class API_Helper
    {
        public static async Task<string> Submit_Request(string requestUri,HttpMethod method)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                App.Current.MainPage.DisplayAlert("No Internet Connection", "Please check your internet connection!","OK");
                return null;
            }
            using (var handler = new HttpClientHandler())
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(Constants.baseAddress);
                var response = await client.GetAsync(requestUri);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public static async Task<string> Search_Query(string query)
        {
            var result = await Submit_Request($"current.json?key={Constants.ApiKey}&q={query}", HttpMethod.Post);
            return result;
        }

        public static async Task<string> get_CityForecast(string query)
        {
            var result = await Submit_Request($"forecast.json?key={Constants.ApiKey}&q={query}&days=5&hour=12", HttpMethod.Post);
            return result;
        }
    }
}
