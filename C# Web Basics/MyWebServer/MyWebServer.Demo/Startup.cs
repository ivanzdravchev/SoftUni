using MyWebServer.Controllers;
using MyWebServer.Data;
using MyWebServer.Server;
using MyWebServer.Server.Controllers;
using MyWebServer.Server.Results.Views;
using System.Threading.Tasks;

namespace MyWebServer
{
    public class Startup
    {
        public static async Task Main()
        {
            var server = HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers()
                    .MapGet<HomeController>("/ToCats", c => c.LocalRedirect()))
                .WithServices(services => services
                    .Add<IData, MyDbContext>()
                    .Add<IViewEngine, CompilationViewEngine>());

            await server.Start();
        }
    }
}
