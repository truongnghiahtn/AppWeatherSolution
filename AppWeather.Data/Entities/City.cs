using System.Collections.Generic;

namespace AppWeather.Data.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string  Name { get; set; }

        public List<History> Histories { get; set; }
    }
}
