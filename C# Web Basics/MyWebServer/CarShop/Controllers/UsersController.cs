using CarShop.Data;
using CarShop.Data.Models;
using CarShop.Models.Users;
using CarShop.Services;
using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;
using System.Linq;

namespace CarShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly IValidator validator;
        private readonly IPasswordHasher passwordHasher;
        private readonly CarShopDbContext data;

        public UsersController(IValidator validator, IPasswordHasher passwordHasher, CarShopDbContext data)
        {
            this.validator = validator;
            this.passwordHasher = passwordHasher;
            this.data = data;
        }

        public HttpResponse Register()
        {
            // if already logged in -> redirect
            if (this.User.Id != null)
            {
                return Redirect("/Cars/All");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterUserFormModel model)
        {
            var modelErrors = this.validator.ValidateUserRegistration(model);

            if (this.data.Users.Any(u => u.Username == model.Username))
            {
                modelErrors.Add($"Username '{model.Username}' already exists.");
            }

            if (this.data.Users.Any(u => u.Email == model.Email))
            {
                modelErrors.Add($"Email '{model.Email}' already in use.");
            }

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var user = new User
            {
                Username = model.Username,
                Password = this.passwordHasher.HashPassword(model.Password),
                Email = model.Email,
                IsMechanic = model.UserType == "Mechanic"
            };

            this.data.Users.Add(user);

            this.data.SaveChanges();

            return Redirect("/Users/Login");
        }

        public HttpResponse Login()
        {
            // if already logged in -> redirect
            if (this.User.Id != null)
            {
                return Redirect("/Cars/All");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Login(LoginUserFormModel model)
        {
            var hashedPassword = this.passwordHasher.HashPassword(model.Password);

            var userId = this.data.Users
                .Where(u => u.Username == model.Username && u.Password == hashedPassword)
                .Select(u => u.Id)
                .FirstOrDefault();

            if (userId == null)
            {
                return Error("Username and password combination is not valid.");
            }

            this.SignIn(userId);

            return Redirect("/Cars/All");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}