using Git.Service;
using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.ViewModels.Users;
using System.Linq;

namespace SharedTrip.Controllers
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
        public HttpResponse Register() => View();

        public HttpResponse Login() => View();

        [HttpPost]
        public HttpResponse Register(RegisterUserFormModel model)
        {
            var modelPass = this.hasher.ComputeSha256Hash(model.Password);
            var errosModel = this.validator.ValidateUser(model);
            if (this.context.Users.Any(u => u.Username == model.Username
            && u.Password == modelPass))
            {
                errosModel.Add("This user is already in db.");
            }
            if (errosModel.Any())
            {
                return Error(errosModel);
            }

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = modelPass
            };

            this.context.Users.Add(user);

            this.context.SaveChanges();

            return Redirect("/Users/Login");
        }

        [HttpPost]
        public HttpResponse Login(LoginUserModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Username)
                || string.IsNullOrWhiteSpace(model.Password))
            {
                return Error("Empty usernamr.");
            }
            var passModel = this.hasher.ComputeSha256Hash(model.Password);

            var userId = this.context.Users
                .Where(u => u.Username == model.Username
                && u.Password == passModel)
                .Select(u => u.Id)
                .FirstOrDefault();

            this.SignIn(userId);

            return Redirect("/Trips/All");
        }

        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/home");
        }
    }
}
