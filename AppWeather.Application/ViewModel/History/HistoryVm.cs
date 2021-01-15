using System;
using System.Collections.Generic;
using System.Text;

namespace AppWeather.Application.ViewModel.History
{
    public class HistoryVm
    {
        public int Id { get; set; }

        public string NameCity { get; set; }

        public string Humidity { get; set; }
        public string Wind { get; set; }
        public string Pressure { get; set; }

        public string Cloudiness { get; set; }

        public DateTime DateCreate { get; set; }
    }
}
