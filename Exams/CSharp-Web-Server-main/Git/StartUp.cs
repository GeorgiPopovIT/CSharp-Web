using Git.Data;
using Git.Service;
using MyWebServer;
using MyWebServer.Controllers;
using MyWebServer.Results.Views;
using System.Threading.Tasks;

namespace Git
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
                   .Add<IValidator, Validator>()
                   .Add<GitDbContext>())
               .Start();
    }
}
