using CarDealer.Data;
using CarDealer.Dtos.Export;
using CarDealer.Dtos.Import;
using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void InitDb(CarDealerContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            string suppliersXml = File.ReadAllText("./Datasets/suppliers.xml");
            ImportSuppliers(db, suppliersXml);

            string partsXml = File.ReadAllText("./Datasets/parts.xml");
            ImportParts(db, partsXml);

            string carsXml = File.ReadAllText("./Datasets/cars.xml");
            ImportCars(db, carsXml);

            string customersXml = File.ReadAllText("./Datasets/customers.xml");
            ImportCustomers(db, customersXml);

            string salesXml = File.ReadAllText("./Datasets/sales.xml");
            ImportSales(db, salesXml);
        }
        public static void Main(string[] args)
        {
            CarDealerContext db = new CarDealerContext();
            //InitDb(db);

            Console.WriteLine(GetSalesWithAppliedDiscount(db));
        }

        // 09. Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(InputSupplierDto[]), new XmlRootAttribute("Suppliers"));

            var suppliersDto = (InputSupplierDto[])serializer.Deserialize(new StringReader(inputXml));

            var suppliers = suppliersDto.Select(s => new Supplier
            {
                Name = s.Name,
                IsImporter = s.IsImporter
            });

            context.Suppliers.AddRange(suppliers);

            context.SaveChanges();

            return $"Successfully imported {suppliers.Count()}";
        }

        // 10. Import Parts
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(InputPartDto[]), new XmlRootAttribute("Parts"));

            var partsDto = (InputPartDto[])serializer.Deserialize(new StringReader(inputXml));

            var supplierIds = context.Suppliers.Select(s => s.Id).ToList();

            var parts = partsDto
                .Where(p => supplierIds.Contains(p.SupplierId))
                .Select(p => new Part
                {
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    SupplierId = p.SupplierId
                });

            context.Parts.AddRange(parts);

            context.SaveChanges();

            return $"Successfully imported {parts.Count()}";
        }

        // 11. Import Cars
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(InputCarDto[]), new XmlRootAttribute("Cars"));

            var carsDto = (InputCarDto[])serializer.Deserialize(new StringReader(inputXml));

            List<Car> cars = new List<Car>();

            var existingPartIds = context.Parts.Select(p => p.Id).ToList();

            foreach (var dto in carsDto)
            {
                var partIds = dto.Parts.Select(p => p.PartId)
                    .Where(p => existingPartIds.Contains(p))
                    .Distinct()
                    .ToList();

                Car car = new Car
                {
                    Make = dto.Make,
                    Model = dto.Model,
                    TravelledDistance = dto.TraveledDistance,
                    PartCars = partIds.Select(partId => new PartCar
                    {
                        PartId = partId
                    })
                    .ToArray()
                };
                cars.Add(car);
            }

            context.Cars.AddRange(cars);

            context.SaveChanges();

            return $"Successfully imported {cars.Count()}";
        }

        // 12. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(InputCustomerDto[]), new XmlRootAttribute("Customers"));

            var customersDto = (InputCustomerDto[])serializer.Deserialize(new StringReader(inputXml));

            var customers = customersDto.Select(c => new Customer
            {
                Name = c.Name,
                BirthDate = c.BirthDate,
                IsYoungDriver = c.IsYoungDriver
            });

            context.Customers.AddRange(customers);

            context.SaveChanges();

            return $"Successfully imported {customers.Count()}";
        }

        // 13. Import Sales
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(InputSaleDto[]), new XmlRootAttribute("Sales"));

            var salesDto = (InputSaleDto[])serializer.Deserialize(new StringReader(inputXml));

            var carIds = context.Cars.Select(c => c.Id).ToList();

            var sales = salesDto
                .Where(s => carIds.Contains(s.CarId))
                .Select(s => new Sale
                {
                    CarId = s.CarId,
                    CustomerId = s.CustomerId,
                    Discount = s.Discount
                });

            context.Sales.AddRange(sales);

            context.SaveChanges();

            return $"Successfully imported {sales.Count()}";
        }

        // 14. Cars With Distance
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.TravelledDistance > 2000000)
                .Select(c => new CarsWithDistanceDto
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(CarsWithDistanceDto[]), new XmlRootAttribute("cars"));

            StringWriter writer = new StringWriter();

            XmlSerializerNamespaces emptyNamespace = new XmlSerializerNamespaces();
            emptyNamespace.Add("", "");

            serializer.Serialize(writer, cars, emptyNamespace);

            return writer.ToString();
        }

        // 15. Cars From Make BMW
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "BMW")
                .Select(c => new BMWCarsDto
                {
                    Id = c.Id,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(BMWCarsDto[]), new XmlRootAttribute("cars"));

            StringWriter writer = new StringWriter();

            XmlSerializerNamespaces emptyNamespace = new XmlSerializerNamespaces();
            emptyNamespace.Add("", "");

            serializer.Serialize(writer, cars, emptyNamespace);

            return writer.ToString();
        }

        // 16. Local Suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => !s.IsImporter)
                .Select(s => new LocalSuppliersDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(LocalSuppliersDto[]), new XmlRootAttribute("suppliers"));

            StringWriter writer = new StringWriter();

            XmlSerializerNamespaces emptyNamespace = new XmlSerializerNamespaces();
            emptyNamespace.Add("", "");

            serializer.Serialize(writer, suppliers, emptyNamespace);

            return writer.ToString();
        }

        // 17. Cars with Their List of Parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new CarsWithPartsDto
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.PartCars.Select(pc => new CarsWithPartsPartDto
                    {
                        Name = pc.Part.Name,
                        Price = pc.Part.Price
                    })
                    .OrderByDescending(p => p.Price)
                    .ToArray()
                })
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(CarsWithPartsDto[]), new XmlRootAttribute("cars"));

            StringWriter writer = new StringWriter();

            XmlSerializerNamespaces emptyNamespace = new XmlSerializerNamespaces();
            emptyNamespace.Add("", "");

            serializer.Serialize(writer, cars, emptyNamespace);

            return writer.ToString();
        }

        // 18. Total Sales by Customer
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Count > 0)
                .Select(c => new TotalSalesByCustomerDto
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count,
                    SpentMoney = c.Sales
                        .Select(s => s.Car)
                        .SelectMany(c => c.PartCars)
                        .Sum(pc => pc.Part.Price)
                })
                .OrderByDescending(c => c.SpentMoney)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(TotalSalesByCustomerDto[]), new XmlRootAttribute("customers"));

            StringWriter writer = new StringWriter();

            XmlSerializerNamespaces emptyNamespace = new XmlSerializerNamespaces();
            emptyNamespace.Add("", "");

            serializer.Serialize(writer, customers, emptyNamespace);

            return writer.ToString();
        }

        // 19. Sales with Applied Discount
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(s => new SalesWithDiscountDto
                {
                    CarDto = new SalesWithDiscountCarDto
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    Discount = s.Discount,
                    CustomerName = s.Customer.Name,
                    Price = s.Car.PartCars.Sum(pc => pc.Part.Price),
                    PriceWithDiscount = s.Car.PartCars.Sum(pc => pc.Part.Price) - s.Car.PartCars.Sum(pc => pc.Part.Price) * s.Discount / 100
                })
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(SalesWithDiscountDto[]), new XmlRootAttribute("sales"));

            StringWriter writer = new StringWriter();

            XmlSerializerNamespaces emptyNamespace = new XmlSerializerNamespaces();
            emptyNamespace.Add("", "");

            serializer.Serialize(writer, sales, emptyNamespace);

            return writer.ToString();
        }
    }
}