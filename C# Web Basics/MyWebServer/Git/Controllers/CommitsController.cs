using Git.Data;
using Git.Data.Models;
using Git.Services;
using Git.ViewModels.Commit;
using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;
using System;
using System.Globalization;
using System.Linq;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly GitDbContext data;


        public CommitsController(GitDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public HttpResponse Create(string id)
        {
            var repository = this.data.Repositories.FirstOrDefault(r => r.Id == id);

            if (repository == null)
            {
                return Error($"Repository with ID: {id} does not exist");
            }

            var commitViewModel = new CreateCommitViewModel
            {
                Id = id,
                Name = repository.Name
            };

            return View(commitViewModel);
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Create(CreateCommitSubmitViewModel model)
        {
            var repository = this.data.Repositories.FirstOrDefault(r => r.Id == model.Id);

            if (repository == null)
            {
                return Error($"Repository with ID: {model.Id} does not exist");
            }

            if (model.Description.Length < 5)
            {
                return Error("Commit message must be longer than 5 symbols");
            }

            var commit = new Commit
            {
                Description = model.Description,
                CreatedOn = DateTime.UtcNow,
                CreatorId = this.User.Id,
                RepositoryId = model.Id
            };

            this.data.Commits.Add(commit);

            this.data.SaveChanges();

            return Redirect("/Repositories/All");
        }

        [Authorize]
        public HttpResponse Delete(string commitId)
        {
            var commit = this.data.Commits.FirstOrDefault(c => c.Id == commitId);

            if (commit == null)
            {
                return Error($"Commit with ID: '{commitId}' does not exist.");
            }

            if (commit.CreatorId != this.User.Id)
            {
                return Error($"You cannot delete commits of other people.");
            }

            this.data.Commits.Remove(commit);

            this.data.SaveChanges();

            return Redirect("/Commits/All");
        }

        [Authorize]
        public HttpResponse All()
        {
            var allCommits = this.data.Commits
                .Where(c => c.CreatorId == this.User.Id)
                .Select(c => new AllCommitsViewModel
                {
                    Id = c.Id,
                    Description = c.Description,
                    CreatedOn = c.CreatedOn.ToString("g", CultureInfo.InvariantCulture),
                    Repository = c.Repository.Name
                })
                .ToList();

            return View(allCommits);
        }
    }
}
