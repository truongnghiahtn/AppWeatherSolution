using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppWeather.Data.EF
{
    public class WeatherDbContextFactory : IDesignTimeDbContextFactory<WeatherDbContext>
    {
        public WeatherDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("WeatherDb");

            var optionsBuilder = new DbContextOptionsBuilder<WeatherDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new WeatherDbContext(optionsBuilder.Options);
        }
    }
}
