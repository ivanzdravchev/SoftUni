using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void InitDb(ProductShopContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            string usersJSON = File.ReadAllText("../../../Datasets/users.json");
            ImportUsers(context, usersJSON);

            string productsJSON = File.ReadAllText("../../../Datasets/products.json");
            ImportProducts(context, productsJSON);

            string categoriesJSON = File.ReadAllText("../../../Datasets/categories.json");
            ImportCategories(context, categoriesJSON);

            string categoriesProductsJSON = File.ReadAllText("../../../Datasets/categories-products.json");
            ImportCategoryProducts(context, categoriesProductsJSON);
        }

        public static void Main(string[] args)
        {
            ProductShopContext db = new ProductShopContext();
            //InitDb(db);

            string result = GetUsersWithProducts(db);
            File.WriteAllText("../../../Results/users-and-products.json", result);
        }

        // 01. Import Users
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            List<User> Users = JsonConvert.DeserializeObject<List<User>>(inputJson);

            context.Users.AddRange(Users);

            context.SaveChanges();

            return $"Successfully imported {Users.Count}";
        }

        // 02. Import Products
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            List<Product> Products = JsonConvert.DeserializeObject<List<Product>>(inputJson);

            context.Products.AddRange(Products);

            context.SaveChanges();

            return $"Successfully imported {Products.Count}";
        }

        // 03. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            List<Category> Categories = JsonConvert.DeserializeObject<List<Category>>(inputJson)
                .Where(c => c.Name != null)
                .ToList();

            context.Categories.AddRange(Categories);

            context.SaveChanges();

            return $"Successfully imported {Categories.Count}";
        }

        // 04. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            List<CategoryProduct> CategoryProducts = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);

            context.CategoryProducts.AddRange(CategoryProducts);

            context.SaveChanges();
            
            return $"Successfully imported {CategoryProducts.Count}";
        }

        // 05. Export Products in Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new
                {
                    p.Name,
                    p.Price,
                    Seller = p.Seller.FirstName + " " + p.Seller.LastName
                })
                .OrderBy(p => p.Price)
                .ToList();

            string result = JsonConvert.SerializeObject(products, settings);

            return result;
        }

        // 06. Export Successfully Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            var users = context.Users
                .Where(u => u.ProductsSold.Count() > 0 && u.ProductsSold.Any(ps => ps.Buyer != null))
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    SoldProducts = u.ProductsSold.Select(sp => new 
                    {
                        sp.Name,
                        sp.Price,
                        BuyerFirstName = sp.Buyer.FirstName,
                        BuyerLastName = sp.Buyer.LastName
                    })
                })
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToList();

            string result = JsonConvert.SerializeObject(users, settings);

            return result;
        }

        // 07. Export Categories by Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            var categories = context.Categories
                .Select(c => new
                {
                    Category = c.Name,
                    ProductsCount = c.CategoryProducts.Count(),
                    AveragePrice = $"{c.CategoryProducts.Average(cp => cp.Product.Price):f2}",
                    TotalRevenue = $"{c.CategoryProducts.Sum(cp => cp.Product.Price):f2}"
                })
                .OrderByDescending(c => c.ProductsCount)
                .ToList();

            string result = JsonConvert.SerializeObject(categories, settings);

            return result;
        }

        // 08. Export Users and Products
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                NullValueHandling = NullValueHandling.Ignore
            };

            var users = context.Users
                .Where(u => u.ProductsSold.Count() > 0 && u.ProductsSold.Any(ps => ps.Buyer != null))
                //.AsEnumerable() for judge
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    u.Age,
                    SoldProducts = new
                    {
                        Count = u.ProductsSold.Count(p => p.Buyer != null),
                        Products = u.ProductsSold
                            .Where(p => p.Buyer != null)
                            .Select(sp => new
                            {
                                sp.Name,
                                sp.Price
                            })
                    }
                })
                .OrderByDescending(u => u.SoldProducts.Count)
                .ToList();

            var solutionObj = new
            {
                UsersCount = users.Count(),
                Users = users
            };

            string result = JsonConvert.SerializeObject(solutionObj, settings);

            return result;
        }
    }
}