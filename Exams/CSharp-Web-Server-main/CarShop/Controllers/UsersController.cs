using CarShop.Data;
using CarShop.Data.Models;
using CarShop.Services;
using CarShop.ViewModels.Users;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;
using static CarShop.Data.Constants;

namespace CarShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly IValidator validator;
        private readonly ApplicationDbContext context;
        private readonly IPasswordHasher hasher;
        public UsersController(ApplicationDbContext context, IPasswordHasher hasher, IValidator validator)
        {
            this.context = context;
            this.hasher = hasher;
            this.validator = validator;
        }
        public HttpResponse Login() => View();

        [HttpPost]
        public HttpResponse Login(LoginFormModel model)
        {
            var userPasword = this.hasher.ComputeSha256Hash(model.Password);

            var userId = this.context.Users
                .Where(u => u.Username == model.Username
                && u.Password == userPasword)
                .Select(u => u.Id)
                .FirstOrDefault();
            if (userId == null)
            {
                return Error("This is invalid user.");
            }

            this.SignIn(userId);

            return Redirect("/Cars/All");
        }

        public HttpResponse Register() => View();

        [HttpPost]
        public HttpResponse Register(RegisterUserFormModel model)
        {
            var errorsModel = this.validator.ValidateUser(model);
            if (this.context.Users.Any(u => u.Username == model.Username))
            {
                errorsModel.Add("This user is already registered.");
            }
            if (errorsModel.Any())
            {
                return Error(errorsModel);
            }

            var hashedPassword = this.hasher.ComputeSha256Hash(model.Password);
            
            
            var user = new User
            {
                Username = model.Username,
                Password = hashedPassword,
                Email = model.Email,
                IsMechanic = model.UserType == UserTypeMechanic
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
