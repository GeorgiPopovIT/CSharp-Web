using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Data;
using SharedTrip.Models;
using SharedTrip.Services;
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

            var errorsModel = this.validator.ValidateUser(model);

            if (this.context.Users.Any(u => u.Username == model.Username
            && u.Password == modelPass))
            {
                errorsModel.Add("This user is already in db.");
            }
            if (this.context.Users.Any(u => u.Username == model.Username))
            {
                errorsModel.Add("This username is reserved");
            }
            if (errorsModel.Any())
            {
                return Error(errorsModel);
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
            var hashedPassword = this.hasher.ComputeSha256Hash(model.Password);

            if (string.IsNullOrWhiteSpace(model.Username)
                || string.IsNullOrWhiteSpace(model.Password))
            {
                return Error("Empty usernamе or passsword.");
            }
            if (!this.context.Users.Any(u => u.Username == model.Username
                && u.Password ==  hashedPassword))
            {
                return Error("Invalid user.Go to register page.");
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
