using BattleCards.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BattleCards.Data
{
    public class BattleCardsDbContext : DbContext
    {
        public BattleCardsDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.; Database=BattleCards; Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCard>()
                .HasKey(uc => new { uc.UserId, uc.CardId });
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<UserCard> UserCards { get; set; }
    }
}
