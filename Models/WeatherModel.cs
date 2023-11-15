using CountryData.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alkan_task.Models
{
    public class WeatherModel
    {
        public string Date { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }
        public string WindSpeed { get; set; }
        public string WeatherCondition { get; set; }
    }
}
