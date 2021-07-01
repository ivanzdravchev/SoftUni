using MyWebServer.Data;
using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;
using System.Linq;

namespace MyWebServer.Controllers
{
    public class CatsController : Controller
    {
        private readonly IData data;

        public CatsController(IData data)
        {
            this.data = data;
        }

        public HttpResponse All()
        {
            var cats = this.data.Cats.ToList();

            return View(cats);
        }

        //public HttpResponse AllHtml()
        //{
        //    var cats = this.data.Cats.ToList();

        //    //var result = "";

        //    return null;
        //}

        [HttpGet]
        public HttpResponse Create() => View();

        [HttpPost]
        public HttpResponse Save(string name, int age)
        {
            return Text($"{name} - {age}");
        }
    }
}
