using Git.Data;
using Git.Data.Models;
using Git.Services;
using Git.ViewModels.Repository;
using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;
using System;
using System.Globalization;
using System.Linq;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IValidator validator;
        private readonly GitDbContext data;

        public RepositoriesController(IValidator validator, GitDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }

        [Authorize]
        public HttpResponse Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Create(CreateRepositoryViewModel model)
        {
            var modelErrors = this.validator.ValidateRepositoryCreation(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var repository = new Repository
            {
                Name = model.Name,
                CreatedOn = DateTime.UtcNow,
                IsPublic = model.RepositoryType == "Public",
                OwnerId = this.User.Id
            };

            this.data.Repositories.Add(repository);

            this.data.SaveChanges();

            return Redirect("/Repositories/All");
        }

        public HttpResponse All()
        {
            var allRepositories = this.data.Repositories
                .Where(r => r.IsPublic || r.OwnerId == this.User.Id)
                .Select(r => new AllRepositoriesViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Owner = r.Owner.Username,
                    CreatedOn = r.CreatedOn.ToString("g", CultureInfo.InvariantCulture),
                    Commits = r.Commits.Count
                })
                .ToList();

            return View(allRepositories);
        }
    }
}
