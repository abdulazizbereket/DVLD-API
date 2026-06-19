using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DVDLDataAccessLayer
{
    public class clsDataAccessLayerSetting
    {
        public static string ConnectionString;

        // Initialize الـ connection string من appsettings.json
        static clsDataAccessLayerSetting()
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                ConnectionString = configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrEmpty(ConnectionString))
                {
                    throw new Exception("Connection String not found in appsettings.json");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في قراءة Connection String: {ex.Message}");
                // Fallback connection string
                ConnectionString = "Server=.;Database=DVLD;Integrated Security=true;";
            }
        }
    }
}