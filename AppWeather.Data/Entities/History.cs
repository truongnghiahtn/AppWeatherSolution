using System;

namespace AppWeather.Data.Entities
{
    public class History
    {
        public int Id { get; set; }

        public Guid IdUser { get; set; }

        public int IdCity { get; set; }

        public string Humidity { get; set; }
        public string Wind { get; set; }
        public string Pressure { get; set; }
        
        public string Cloudiness { get; set; }

        public DateTime DateCreate { get; set; }

        public AppUser AppUser { get; set; }

        public City City { get; set; }
    }
}
