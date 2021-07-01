using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;
using System;

namespace MyWebServer.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index()
        {
            return Text("Hello from Ivan!");
        }

        public HttpResponse LocalRedirect()
        {
            return Redirect("/Animals/Cats");
        }

        public HttpResponse ToSoftuni()
        {
            return Redirect("https://www.softuni.bg");
        }

        public HttpResponse StaticFiles()
        {
            return View();
        }

        public HttpResponse Error()
        {
            throw new InvalidOperationException("Invalid action!");
        }
    }
}
