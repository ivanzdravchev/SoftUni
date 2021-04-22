using RealEstates.Data;
using RealEstates.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace RealEstates.Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            ImportJsonFile("imot.bg-houses-Sofia-raw-data-2021-03-18.json");
            ImportJsonFile("imot.bg-raw-data-2021-03-18.json");
        }

        static void ImportJsonFile(string jsonFilePath)
        {
            var context = new ApplicationDbContext();
            IPropertiesService propertiesService = new PropertiesService(context);

            var properties = JsonSerializer.Deserialize<IEnumerable<PropertyAsJson>>(
                File.ReadAllText(jsonFilePath));

            foreach (var prop in properties)
            {
                propertiesService.Add(prop.District, prop.Floor, prop.TotalFloors, prop.Size,
                    prop.YardSize, prop.Year, prop.PropertyType, prop.BuildingType, prop.Price);

                Console.Write(".");
            }
        }
    }
}
