using System;
using System.Collections.Generic;
using System.Text;

namespace AppWeather.Application.ViewModel.History
{
    public class CreateHistoryRequest
    {

        public Guid IdUser { get; set; }

        public int IdCity { get; set; }

        public string Humidity { get; set; }
        public string Wind { get; set; }
        public string Pressure { get; set; }

        public string Cloudiness { get; set; }
    }
}
