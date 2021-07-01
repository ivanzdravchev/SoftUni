using MyWebServer.Server.Http;

namespace MyWebServer.Server.Controllers
{
    public class HttpPostAttribute : HttpMethodAttribute
    {
        public HttpPostAttribute()
            : base(HttpMethod.Post)
        {
        }
    }
}
