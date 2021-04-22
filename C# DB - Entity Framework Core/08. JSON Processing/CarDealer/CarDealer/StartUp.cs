using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static void InitDb(CarDealerContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            string suppliersJson = File.ReadAllText("../../../Datasets/suppliers.json");
            ImportSuppliers(db, suppliersJson);

            string partsJson = File.ReadAllText("../../../Datasets/parts.json");
            ImportParts(db, partsJson);

            string carsJson = File.ReadAllText("../../../Datasets/cars.json");
            ImportCars(db, carsJson);

            string customersJson = File.ReadAllText("../../../Datasets/customers.json");
            ImportCustomers(db, customersJson);

            string salesJson = File.ReadAllText("../../../Datasets/sales.json");
            ImportCustomers(db, salesJson);
        }

        public static void Main(string[] args)
        {
            CarDealerContext db = new CarDealerContext();
            //InitDb(db);

            Console.WriteLine(GetSalesWithAppliedDiscount(db));
        }

        // 09. Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var Suppliers = JsonConvert.DeserializeObject<IEnumerable<Supplier>>(inputJson);

            context.Suppliers.AddRange(Suppliers);

            context.SaveChanges();

            return $"Successfully imported {Suppliers.Count()}.";
        }

        // 10. Import Parts
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var validSupplierIds = context.Suppliers.Select(s => s.Id).ToList();

            var Parts = JsonConvert.DeserializeObject<IEnumerable<Part>>(inputJson)
                .Where(x => validSupplierIds.Contains(x.SupplierId));

            context.Parts.AddRange(Parts);

            context.SaveChanges();

            return $"Successfully imported {Parts.Count()}.";
        }

        // 11. Import Cars
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var CarsDTO = JsonConvert.DeserializeObject<IEnumerable<CarsDTO>>(inputJson);

            var Cars = new List<Car>();

            foreach (var carDto in CarsDTO)
            {
                var currentCar = new Car
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TravelledDistance = carDto.TravelledDistance
                };

                foreach (var partId in carDto.PartsId.Distinct())
                {
                    currentCar.PartCars.Add(new PartCar 
                    {
                        PartId = partId
                    });
                }

                Cars.Add(currentCar);
            }

            context.Cars.AddRange(Cars);

            context.SaveChanges();

            return $"Successfully imported {Cars.Count}.";
        }

        // 12. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var Customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(inputJson);

            context.Customers.AddRange(Customers);

            context.SaveChanges();

            return $"Successfully imported {Customers.Count()}.";
        }

        // 13. Import Sales
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var Sales = JsonConvert.DeserializeObject<List<Sale>>(inputJson);

            context.Sales.AddRange(Sales);

            context.SaveChanges();

            return $"Successfully imported {Sales.Count}.";
        }

        // 14. Export Ordered Customers
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new 
                {
                    c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    c.IsYoungDriver
                })
                .ToList();

            return JsonConvert.SerializeObject(customers, Formatting.Indented);
        }

        // 15. Export Cars from Make Toyota
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "Toyota")
                .Select(c => new 
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TravelledDistance
                })
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToList();

            return JsonConvert.SerializeObject(cars, Formatting.Indented);
        }

        // 16. Export Local Suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToList();

            return JsonConvert.SerializeObject(suppliers, Formatting.Indented);
        }

        // 17. Export Cars with Their List of Parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new 
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TravelledDistance
                    },
                    parts = c.PartCars.Select(pc => new 
                    {
                        pc.Part.Name,
                        Price = $"{pc.Part.Price:f2}"
                    })
                })
                .ToList();

            return JsonConvert.SerializeObject(cars, Formatting.Indented);
        }

        // 18. Export Total Sales by Customer
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Count > 0)
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count,
                    spentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price))
                })
                .ToList();

            return JsonConvert.SerializeObject(customers, Formatting.Indented);
        }

        // 19. Export Sales with Applied Discount
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(s => new
                {
                    car = new
                    {
                        s.Car.Make,
                        s.Car.Model,
                        s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    Discount = $"{s.Discount:f2}",
                    price = $"{s.Car.PartCars.Sum(pc => pc.Part.Price):f2}",
                    priceWithDiscount = $"{s.Car.PartCars.Sum(pc => pc.Part.Price) / 100 * (100 - s.Discount):f2}"
                })
                .Take(10)
                .ToList();

            return JsonConvert.SerializeObject(sales, Formatting.Indented);
        }
    }
}