using BattleCards.Data;
using BattleCards.Services;
using Microsoft.EntityFrameworkCore;
using MyWebServer.Server;
using MyWebServer.Server.Controllers;
using MyWebServer.Server.Results.Views;
using System.Threading.Tasks;

namespace BattleCards
{
    public class Startup
    {
        public async static Task Main()
        {
            var server = HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers())
                .WithServices(services => services
                    .Add<IViewEngine, CompilationViewEngine>()
                    .Add<IValidator, Validator>()
                    .Add<IPasswordHasher, PasswordHasher>()
                    .Add<BattleCardsDbContext>())
                .WithConfiguration<BattleCardsDbContext>(context => context.Database.Migrate());

            await server.Start();
        }
    }
}
