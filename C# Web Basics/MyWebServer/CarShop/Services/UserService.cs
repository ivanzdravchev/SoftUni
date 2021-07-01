using CarShop.Data;
using System.Linq;

namespace CarShop.Services
{
    public class UserService : IUserService
    {
        private readonly CarShopDbContext data;

        public UserService(CarShopDbContext data)
        {
            this.data = data;
        }

        public bool UserIsMechanic(string userId)
        {
            return this.data.Users.Any(u => u.Id == userId && u.IsMechanic);
        }
    }
}
