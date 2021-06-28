using BattleCards.Data;
using BattleCards.Services;
using MyWebServer;
using MyWebServer.Controllers;
using MyWebServer.Results.Views;
using System.Threading.Tasks;

namespace BattleCards
{
    public class StartUp
    {
        public static async Task Main()
           => await HttpServer
               .WithRoutes(routes => routes
                   .MapStaticFiles()
                   .MapControllers())
               .WithServices(services => services
                   .Add<IViewEngine, CompilationViewEngine>()
                   .Add<IValidator, Validator>()
                   .Add<IPasswordHasher,PasswordHasher>()
                   .Add<ApplicationDbContext>())
               .Start();
    }
}
