using BattleCards.Data;
using BattleCards.Models;
using BattleCards.Services;
using BattleCards.ViewModels;
using BattleCards.ViewModels.Users;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace BattleCards.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IValidator validator;
        private readonly IPasswordHasher hasher;
        public UsersController(ApplicationDbContext context, IValidator validator, IPasswordHasher hasher)
        {
            this.context = context;
            this.validator = validator;
            this.hasher = hasher;
        }
        public HttpResponse Login() => this.View();

        [HttpPost]
        public HttpResponse Login(LoginUserModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password)) 
            {
                return Error("Username or password are empty.");
            }
            var currUserPassword = this.hasher.ComputeSha256Hash(model.Password);

            var userId = this.context.Users.Where(u => u.Password == currUserPassword
            && u.Username == model.Username).Select(u => u.Id)
                .FirstOrDefault();

            if (userId == null)
            {
                return Error("This user is not already registered.");
            }

            this.SignIn(userId);

            return Redirect("/Cards/All");
        }

        public HttpResponse Register() => this.View();

        [HttpPost]
        public HttpResponse Register(RegisterUserModel model)
        {
            var errorsModel = this.validator.ValidateUser(model);
            if (errorsModel.Any())
            {
                return Redirect("/Users/Register");
            }
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = this.hasher.ComputeSha256Hash(model.Password)
            };

            this.context.Users.Add(user);

            this.context.SaveChanges();

            return Redirect("/Users/Login");
        }


        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}
