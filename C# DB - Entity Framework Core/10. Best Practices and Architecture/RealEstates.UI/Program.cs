using Microsoft.EntityFrameworkCore;
using RealEstates.Data;
using RealEstates.Services;
using RealEstates.Services.DTOs;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RealEstates.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            ApplicationDbContext context = new ApplicationDbContext();
            context.Database.Migrate();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Property search");
                Console.WriteLine("2. Most Expensive Districts");
                Console.WriteLine("3. Average price per square meter");
                Console.WriteLine("4. Add tag");
                Console.WriteLine("5. Bulk add tag to properties");
                Console.WriteLine("6. Property full info");

                bool parsed = int.TryParse(Console.ReadLine(), out int option);
                if (parsed && option == 0)
                {
                    break;
                }

                if (parsed && option >= 1 && option <= 6)
                {
                    switch (option)
                    {
                        case 1:
                            PropertySearch(context);
                            break;
                        case 2:
                            MostExpensiveDistricts(context);
                            break;
                        case 3:
                            AveragePricePerSquareMeter(context);
                            break;
                        case 4:
                            AddTag(context);
                            break;
                        case 5:
                            BulkTagToProperties(context);
                            break;
                        case 6:
                            PropertyFullInfo(context);
                            break;
                        default:
                            break;
                    }
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static void PropertySearch(ApplicationDbContext context)
        {
            Console.Write("Min price: ");
            int minPrice = int.Parse(Console.ReadLine());

            Console.Write("Max price: ");
            int maxPrice = int.Parse(Console.ReadLine());

            Console.Write("Min size: ");
            int minSize = int.Parse(Console.ReadLine());

            Console.Write("Max size: ");
            int maxSize = int.Parse(Console.ReadLine());

            IPropertiesService propertiesService = new PropertiesService(context);

            var properties = propertiesService.Search(minPrice, maxPrice, minSize, maxSize);

            foreach (var property in properties)
            {
                Console.WriteLine($"{property.DistrictName}; {property.BuildingType}; {property.PropertyType} => {property.Price}€ => {property.Size}m²");
            }
        }

        private static void MostExpensiveDistricts(ApplicationDbContext context)
        {
            Console.Write("Districts count: ");
            int count = int.Parse(Console.ReadLine());

            IDistrictsService districtService = new DistrictsService(context);

            var districts = districtService.GetMostExpensiveDistricts(count);

            foreach (var district in districts)
            {
                Console.WriteLine($"{district.Name} => {district.PropertiesCount} properties with {district.AveragePricePerSquareMeter:f2}€/m² average price");
            }
        }

        private static void AveragePricePerSquareMeter(ApplicationDbContext context)
        {
            IPropertiesService propertiesService = new PropertiesService(context);

            Console.WriteLine($"Average price per square meter: {propertiesService.AveragePricePerSquareMeter():f2}€/m²");
        }

        private static void AddTag(ApplicationDbContext context)
        {
            IPropertiesService propertiesService = new PropertiesService(context);
            ITagService tagService = new TagService(context, propertiesService);

            Console.WriteLine("Tag name: ");
            string tagName = Console.ReadLine();

            Console.WriteLine("Importance (optional): ");
            bool isImportanceParsed = int.TryParse(Console.ReadLine(), out int importance);
            int? tagImportance = isImportanceParsed ? importance : null;

            tagService.Add(tagName, tagImportance);
        }

        private static void BulkTagToProperties(ApplicationDbContext context)
        {
            IPropertiesService propertiesService = new PropertiesService(context);
            ITagService tagService = new TagService(context, propertiesService);

            Console.WriteLine("Bulk operation started!");
            tagService.BulkTagToProperties();
            Console.WriteLine("Bulk operation finished!");
        }

        private static void PropertyFullInfo(ApplicationDbContext context)
        {
            Console.Write("Count of properties: ");
            int count = int.Parse(Console.ReadLine());

            IPropertiesService propertiesService = new PropertiesService(context);
            var result = propertiesService.GetFullData(count).ToArray();

            var xmlSerializer = new XmlSerializer(typeof(PropertyInfoFullDataDto[]));

            var stringWriter = new StringWriter();
            xmlSerializer.Serialize(stringWriter, result);

            Console.WriteLine(stringWriter.ToString());
        }
    }
}
