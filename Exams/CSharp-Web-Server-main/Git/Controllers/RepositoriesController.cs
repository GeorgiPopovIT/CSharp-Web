using Git.Data;
using Git.Data.Models;
using Git.Service;
using Git.ViewModels.Repositories;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;
using static Git.Common.Constants;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IValidator validator;
        private readonly GitDbContext context;
        public RepositoriesController(IValidator validator, GitDbContext context)
        {
            this.context = context;
            this.validator = validator;
        }
        public HttpResponse All()
        {
            var repositories = this.context.Repositories
                .Select(r => new RepositoryListringViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    CreatedOn = r.CreatedOn,
                    Owner = r.Owner.UserName,
                    CommitsCount = r.Commits.Count()
                }).ToList();

            return View(repositories);
        }
        [Authorize]
        public HttpResponse Create() => View();
        [Authorize]
        [HttpPost]
        public HttpResponse Create(CreateRepositoryViewModel model)
        {
            var modelErrors = this.validator.ValidateRepository(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }
            var repository = new Repository
            {
                Name = model.Name,
                IsPublic = model.RepositoryType == PublicType,
                OwnerId = this.User.Id
            };

            this.context.Repositories.Add(repository);

            this.context.SaveChanges();

            return Redirect("/Repositories/All");
        }
    }
}
