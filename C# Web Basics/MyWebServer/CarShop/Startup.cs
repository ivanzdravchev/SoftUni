using CarShop.Data;
using CarShop.Services;
using Microsoft.EntityFrameworkCore;
using MyWebServer.Server;
using MyWebServer.Server.Controllers;
using MyWebServer.Server.Results.Views;
using System.Threading.Tasks;

namespace CarShop
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
                    .Add<IUserService, UserService>()
                    .Add<CarShopDbContext>())
                .WithConfiguration<CarShopDbContext>(context => context.Database.Migrate());

            await server.Start();
        }
    }
}
