using CarShop.Data;
using CarShop.Data.Models;
using CarShop.Models.Issues;
using CarShop.Services;
using Microsoft.EntityFrameworkCore;
using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;
using System.Linq;

namespace CarShop.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IUserService userService;
        private readonly IValidator validator;
        private readonly CarShopDbContext data;

        public IssuesController(IUserService userService, IValidator validator, CarShopDbContext data)
        {
            this.userService = userService;
            this.validator = validator;
            this.data = data;
        }

        [Authorize]
        public HttpResponse CarIssues(string carId)
        {
            if (!this.userService.UserIsMechanic(this.User.Id))
            {
                var userOwnsCar = this.data.Cars
                    .Any(c => c.Id == carId && c.OwnerId == this.User.Id);

                if (!userOwnsCar)
                {
                    return Error("You do not have access to this car.");
                }
            }

            var carWithIssues = this.data.Cars
                .Where(c => c.Id == carId)
                .Select(c => new CarIssuesViewModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    Year = c.Year,
                    Issues = c.Issues.Select(i => new IssueListingViewModel
                    {
                        Id = i.Id,
                        Description = i.Description,
                        IsFixed = i.IsFixed
                    })
                })
                .FirstOrDefault();

            if (carWithIssues == null)
            {
                return Error($"Car with ID '{carId}' does not exist");
            }

            return View(carWithIssues);
        }

        [Authorize]
        public HttpResponse Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddIssueViewModel model, string carId)
        {
            if (this.userService.UserIsMechanic(this.User.Id))
            {
                return Unauthorized();
            }

            var car = this.data.Cars
                .FirstOrDefault(c => c.Id == carId);

            var modelErrors = this.validator.ValidateIssueCreation(model);

            if (car == null)
            {
                modelErrors.Add($"Car with {carId} does not exist.");
            }

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }
            
            var newIssue = new Issue
            {
                Description = model.Description,
                CarId = carId,
                IsFixed = false
            };

            car.Issues.Add(newIssue);

            this.data.SaveChanges();

            return Redirect("/Cars/All");
        }

        [Authorize]
        public HttpResponse Fix(string issueId, string carId)
        {
            if (!this.userService.UserIsMechanic(this.User.Id))
            {
                return Unauthorized();
            }

            var car = this.data.Cars
                .Include(c => c.Issues)
                .FirstOrDefault(c => c.Id == carId);

            if (car == null)
            {
                return Error($"Car with ID: '{carId}' does not exist.");
            }

            var carIssue = car.Issues.FirstOrDefault(i => i.Id == issueId);

            if (carIssue == null)
            {
                return Error($"Issue with ID: '{issueId}' does not exist.");
            }

            carIssue.IsFixed = true;

            this.data.SaveChanges();

            return Redirect($"/Issues/CarIssues?carId={carId}");
        }

        [Authorize]
        public HttpResponse Delete(string issueId, string carId)
        {
            var car = this.data.Cars
                .Include(c => c.Issues)
                .FirstOrDefault(c => c.Id == carId);

            if (car == null)
            {
                return Error($"Car with ID: '{carId}' does not exist.");
            }

            var carIssue = car.Issues.FirstOrDefault(i => i.Id == issueId);

            if (carIssue == null)
            {
                return Error($"Issue with ID: '{issueId}' does not exist.");
            }

            if (car.OwnerId != this.User.Id)
            {
                return Error($"Cannot delete issues that are not your own.");
            }

            car.Issues.Remove(carIssue);

            this.data.SaveChanges();

            return Redirect($"/Issues/CarIssues?carId={carId}");
        }
    }
}