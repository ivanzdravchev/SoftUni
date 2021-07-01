using MyWebServer.Models.Animals;
using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;

namespace MyWebServer.Controllers
{
    public class DogsController : Controller
    {
        [HttpGet]
        public HttpResponse Create()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Create(DogFormModel model)
        {
            return Text($"Dog: {model.Name} - {model.Age} - {model.Breed}");
        }
    }
}
