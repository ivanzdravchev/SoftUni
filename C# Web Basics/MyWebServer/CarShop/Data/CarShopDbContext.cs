using CarShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Data
{
    public class CarShopDbContext : DbContext
    {
        public CarShopDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.; Database=Carshop; Integrated Security=true");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; init; }

        public DbSet<Car> Cars { get; init; }

        public DbSet<Issue> Issues { get; init; }
    }
}
