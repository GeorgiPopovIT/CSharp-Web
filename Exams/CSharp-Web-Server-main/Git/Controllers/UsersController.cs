using Git.Data;
using Git.Data.Models;
using Git.Service;
using Git.ViewModels.Users;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace Git.Controllers
{
    public class UsersController : Controller
    {
        private readonly IValidator validator;
        private readonly GitDbContext context;
        private readonly IPasswordHasher hasher;
        public UsersController(GitDbContext context, IPasswordHasher hasher, IValidator validator)
        {
            this.context = context;
            this.hasher = hasher;
            this.validator = validator;
        }
        public HttpResponse Login() => View();
        [HttpPost]
        public HttpResponse Login(LoginUserModel model)
        {
            var userId = this.context.Users.Where(u => u.UserName == model.Username &&
            u.Password == hasher.ComputeSha256Hash(model.Password))
                .Select(u => u.Id)
                .FirstOrDefault();
            if (userId == null)
            {
                return Error("This user is not registrated.");
            }
            this.SignIn(userId);

            return Redirect("/Repositories/All");
        }

        public HttpResponse Register() => View();

        [HttpPost]
        public HttpResponse Register(RegisterUserFormModel model)
        {
            var modelErrors = this.validator.ValidateUser(model);
            if (this.context.Users.Any(u => u.UserName == model.Username))
            {
                modelErrors.Add("This user is already registered.");
            }

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }
            var user = new User
            {
                UserName = model.Username,
                Password = this.hasher.ComputeSha256Hash(model.Password),
                Email = model.Email
            };

            this.context.Users.Add(user);

            this.context.SaveChanges();

            return Redirect("/Users/Login");
        }
       
        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/home");
        }
    }
}
