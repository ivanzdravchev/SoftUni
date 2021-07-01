using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;

namespace Git.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index()
        {
            if (this.User.IsAuthenticated)
            {
                return Redirect("/Repositories/All");
            }

            return View();
        }
    }
}
