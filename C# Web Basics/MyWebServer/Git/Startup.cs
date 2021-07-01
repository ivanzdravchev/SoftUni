using Git.Data;
using Git.Services;
using Microsoft.EntityFrameworkCore;
using MyWebServer.Server;
using MyWebServer.Server.Controllers;
using MyWebServer.Server.Results.Views;
using System.Threading.Tasks;

namespace Git
{
    public class Startup
    {
        public static async Task Main()
        {
            var server = HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers())
                .WithServices(services => services
                    .Add<IViewEngine, CompilationViewEngine>()
                    .Add<IValidator, Validator>()
                    .Add<IPasswordHasher, PasswordHasher>()
                    .Add<GitDbContext>())
                .WithConfiguration<GitDbContext>(context => context.Database.Migrate());

            await server.Start();
        }
    }
}
