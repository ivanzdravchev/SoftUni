using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;

namespace BattleCards.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index()
        {
            if (this.User.IsAuthenticated)
            {
                return Redirect("/Cards/All");
            }

            return View();
        }
    }
}
