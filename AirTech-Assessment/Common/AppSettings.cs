using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace AirTech_Assessment.Common
{
    /// <summary>
    /// The class that deals with the configuration settings taken from
    /// appsettings.json
    /// </summary>
    public class AppSettings
    {

        public string SchedulePath { get; set; }
       
        public string OrderPath { get; set; }
       

        public int PlaneLoadCapacity { get; set; }

        public static AppSettings GetSettings()
        {
            var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            var settings = new AppSettings();
            configuration.GetSection("Configs").Bind(settings);
            return settings;
        }

    }
}
