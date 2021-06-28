using Git.Data;
using Git.Data.Models;
using Git.Service;
using Git.ViewModels.Commits;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Linq;
using static Git.Common.Constants;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly GitDbContext context;
        public CommitsController(GitDbContext context)
        {
            this.context = context;
        }

        [Authorize]
        public HttpResponse Create(string id)
        {
            var repository = this.context.Repositories
                .Where(r => r.Id == id)
                .Select(r => new CommitToRepoViewModel
                {
                    Id = r.Id,
                    Repository = r.Name
                })
                .FirstOrDefault();
            if (repository == null)
            {
                return Error("This repo is null.");
            }
            return View(repository);
        }

        [Authorize]
        [HttpPost]   
        public HttpResponse Create(CreateCommitViewModel model)
        {
            if (!this.context.Repositories.Any(r => r.Id == model.Id))
            {
                return BadRequest();
            }
            if (model.Description.Length < CommitDescriptionMinLength)
            {
                return Error("Desccription is short.");
            }

            var commit = new Commit
            {
                Description = model.Description,
                CreatedOn = DateTime.UtcNow,
                CreatorId = this.User.Id,
                RepositoryId = model.Id
            };

            this.context.Commits.Add(commit);

            this.context.SaveChanges();

            return Redirect("/Repositories/All");
        }

        [Authorize]
        public HttpResponse All()
        {
            var commits = this.context.Commits
                .Where(c => c.CreatorId == this.User.Id)
                .Select(c => new ListingCommitViewModel
                {
                    Id = c.Id,
                    Description = c.Description,
                    CreatedOn = c.CreatedOn,
                    Name = c.Repository.Name
                })
                .ToList();

            return View(commits);
        }

        [Authorize]
        public HttpResponse Delete(string id)
        {
            var commit = this.context.Commits.FirstOrDefault(c => c.Id == id);

            if (commit == null)
            {
                return Error("Cannot delete the commit.");
            }

            this.context.Commits.Remove(commit);

            this.context.SaveChanges();

            return Redirect("/Commits/All");
        }
    }
}
