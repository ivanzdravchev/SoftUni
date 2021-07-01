using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.Services;
using SharedTrip.ViewModels.User;
using System.Linq;

namespace SharedTrip.Controllers
{
    public class UsersController : Controller
    {
        private readonly IValidator validator;
        private readonly IPasswordHasher passwordHasher;
        private readonly ApplicationDbContext data;

        public UsersController(IValidator validator, IPasswordHasher passwordHasher, ApplicationDbContext data)
        {
            this.validator = validator;
            this.passwordHasher = passwordHasher;
            this.data = data;
        }

        public HttpResponse Register()
        {
            if (this.User.IsAuthenticated)
            {
                return Redirect("/Trips/All");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterUserViewModel model)
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
                Email = model.Email
            };

            this.data.Users.Add(user);

            this.data.SaveChanges();

            return Redirect("/Users/Login");
        }

        public HttpResponse Login()
        {
            if (this.User.IsAuthenticated)
            {
                return Redirect("/Trips/All");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Login(LoginUserViewModel model)
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

            return Redirect("/Trips/All");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}
