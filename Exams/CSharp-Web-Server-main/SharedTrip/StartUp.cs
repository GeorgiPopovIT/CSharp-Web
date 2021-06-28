using Git.Service;
using MyWebServer;
using MyWebServer.Controllers;
using MyWebServer.Results.Views;
using SharedTrip.Data;
using System.Threading.Tasks;

namespace SharedTrip
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
                   .Add<IPasswordHasher, PasswordHasher>()
                   .Add<IValidator, ValidatorClass>()
                   .Add<ApplicationDbContext>())
               .Start();
    }
}
