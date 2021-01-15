using System;
using System.Collections.Generic;
using System.Text;

namespace AppWeather.Application.Service.ServiceHistory
{
    public class HistoryByDate<T>
    {
        public DateTime DateCreate { get; set; }

        public List<T> DataCitys { get; set; }
    }
}
