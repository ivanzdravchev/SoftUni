using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;

namespace CarShop.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index()
        {
            if (this.User.IsAuthenticated)
            {
                return Redirect("/Cars/All");
            }

            return View();
        }
    }
}