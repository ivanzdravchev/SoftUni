using CarShop.Data;
using CarShop.Data.Models;
using CarShop.Models.Car;
using CarShop.Services;
using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;
using System.Linq;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly IValidator validator;
        private readonly IUserService userService;
        private readonly CarShopDbContext data;

        public CarsController(IValidator validator, IUserService userService, CarShopDbContext data)
        {
            this.validator = validator;
            this.userService = userService;
            this.data = data;
        }

        [Authorize]
        public HttpResponse Add()
        {
            if (this.userService.UserIsMechanic(this.User.Id))
            {
                return Unauthorized();
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddCarFormModel model)  
        {
            if (this.userService.UserIsMechanic(this.User.Id))
            {
                return Unauthorized();
            }

            var modelErrors = this.validator.ValidateCarCreation(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var car = new Car
            {
                Model = model.Model,
                Year = model.Year,
                PictureUrl = model.Image,
                PlateNumber = model.PlateNumber,
                OwnerId = this.User.Id
            };

            this.data.Cars.Add(car);

            this.data.SaveChanges();

            return Redirect("/Cars/All");
        }

        [Authorize]
        public HttpResponse All()
        {
            var carsQuery = this.data.Cars.AsQueryable();

            if (this.userService.UserIsMechanic(this.User.Id))
            {
                carsQuery = carsQuery.Where(c => c.Issues.Any(i => !i.IsFixed));
            }
            else
            {
                carsQuery = carsQuery.Where(c => c.OwnerId == this.User.Id);
            }

            var cars = carsQuery
                .Select(c => new CarListingViewModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    Year = c.Year,
                    Image = c.PictureUrl,
                    PlateNumber = c.PlateNumber,
                    FixedIssues = c.Issues.Where(i => i.IsFixed).Count(),
                    RemainingIssues = c.Issues.Where(i => !i.IsFixed).Count()
                })
                .ToList();

            return View(cars);
        }
    }
}
