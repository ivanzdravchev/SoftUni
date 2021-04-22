using ProductShop.Data;
using ProductShop.Dtos.Import;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using ProductShop.Models;
using System;
using ProductShop.Dtos.Export;
using ProductShop.Dtos.Export.SoldProducts;
using ProductShop.Dtos.Export.UsersWithProducts;

namespace ProductShop
{
    public class StartUp
    {
        public static void InitDb(ProductShopContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            string usersXml = File.ReadAllText("./Datasets/users.xml");
            ImportUsers(db, usersXml);

            string productsXml = File.ReadAllText("./Datasets/products.xml");
            ImportProducts(db, productsXml);

            string categoriesXml = File.ReadAllText("./Datasets/categories.xml");
            ImportCategories(db, categoriesXml);

            string categoryProducts = File.ReadAllText("./Datasets/categories-products.xml");
            ImportCategoryProducts(db, categoryProducts);
        }
        public static void Main(string[] args)
        {
            ProductShopContext db = new ProductShopContext();
            //InitDb(db);

            Console.WriteLine(GetUsersWithProducts(db));
        }

        // 01. Import Users
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportUserDto[]), new XmlRootAttribute("Users"));

            var usersDto = (ImportUserDto[])serializer.Deserialize(new StringReader(inputXml));

            var users = usersDto.Select(u => new User
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Age = u.Age
            });

            context.Users.AddRange(users);

            context.SaveChanges();

            return $"Successfully imported {users.Count()}";
        }

        // 02. Import Products
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportProductDto[]), new XmlRootAttribute("Products"));

            var productsDto = (ImportProductDto[])serializer.Deserialize(new StringReader(inputXml));

            var products = productsDto.Select(p => new Product
            {
                Name = p.Name,
                Price = p.Price,
                SellerId = p.SellerId,
                BuyerId = p.BuyerId
            });

            context.Products.AddRange(products);

            context.SaveChanges();

            return $"Successfully imported {products.Count()}";
        }

        // 03. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportCategoryDto[]), new XmlRootAttribute("Categories"));

            var categoriesDto = (ImportCategoryDto[])serializer.Deserialize(new StringReader(inputXml));

            var categories = categoriesDto.Where(c => c.Name != null).Select(c => new Category
            {
                Name = c.Name
            });

            context.Categories.AddRange(categories);

            context.SaveChanges();
            
            return $"Successfully imported {categories.Count()}";
        }

        // 04. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportCategoryProductDto[]), new XmlRootAttribute("CategoryProducts"));

            var categoryProductsDto = (ImportCategoryProductDto[])serializer.Deserialize(new StringReader(inputXml));

            var categoryIds = context.Categories.Select(c => c.Id).ToList();
            var productIds = context.Products.Select(p => p.Id).ToList();

            var categoryProducts = categoryProductsDto
                .Where(cp => categoryIds.Contains(cp.CategoryId) && productIds.Contains(cp.ProductId))
                .Select(cp => new CategoryProduct
                {
                    CategoryId = cp.CategoryId,
                    ProductId = cp.ProductId
                });

            context.CategoryProducts.AddRange(categoryProducts);

            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count()}";
        }

        // 05. Products In Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new ProductsInRangeDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    Buyer = p.Buyer.FirstName + " " + p.Buyer.LastName
                })
                .OrderBy(p => p.Price)
                .Take(10)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ProductsInRangeDto[]), new XmlRootAttribute("Products"));

            StringWriter writer = new StringWriter();

            XmlSerializerNamespaces emptyNamespace = new XmlSerializerNamespaces();
            emptyNamespace.Add("", "");

            serializer.Serialize(writer, products, emptyNamespace);

            return writer.ToString();
        }

        // 06. Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Count > 0)
                .Select(u => new UserDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold.Select(ps => new ProductDto
                    {
                        Name = ps.Name,
                        Price = ps.Price
                    })
                    .ToArray()
                })
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("Users"));

            StringWriter writer = new StringWriter();

            XmlSerializerNamespaces emptyNamespace = new XmlSerializerNamespaces();
            emptyNamespace.Add("", "");

            serializer.Serialize(writer, users, emptyNamespace);

            return writer.ToString();
        }

        // 07. Categories By Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(c => new CategoriesByProductsDto
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Average(s => s.Product.Price),
                    TotalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price)
                })
                .OrderByDescending(c => c.Count)
                .ThenBy(c => c.TotalRevenue)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(CategoriesByProductsDto[]), new XmlRootAttribute("Categories"));

            StringWriter writer = new StringWriter();

            XmlSerializerNamespaces emptyNamespace = new XmlSerializerNamespaces();
            emptyNamespace.Add("", "");

            serializer.Serialize(writer, categories, emptyNamespace);

            return writer.ToString();
        }

        // 08. Users and Products
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = new AllUsersDto
            {
                Count = context.Users.Where(u => u.ProductsSold.Count > 0).Count(),
                Users = context.Users
                    .Where(u => u.ProductsSold.Count > 0)
                    .ToArray()
                    .Select(u => new FullUserDto
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Age = u.Age,
                        SoldProducts = new SoldProductsDto
                        {
                            Count = u.ProductsSold.Count,
                            Products = u.ProductsSold.Select(ps => new UserProductDto
                            {
                                Name = ps.Name,
                                Price = ps.Price
                            })
                            .OrderByDescending(p => p.Price)
                            .ToArray()
                        }
                    })
                    .OrderByDescending(u => u.SoldProducts.Count)
                    .Take(10)
                    .ToArray()
            };

            XmlSerializer serializer = new XmlSerializer(typeof(AllUsersDto), new XmlRootAttribute("Users"));

            StringWriter writer = new StringWriter();

            XmlSerializerNamespaces emptyNamespace = new XmlSerializerNamespaces();
            emptyNamespace.Add("", "");

            serializer.Serialize(writer, users, emptyNamespace);

            return writer.ToString();
        }
    }
}