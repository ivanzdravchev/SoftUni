using MyWebServer.Server.Http;

namespace MyWebServer.Server.Controllers
{
    public class HttpGetAttribute : HttpMethodAttribute
    {
        public HttpGetAttribute()
            : base(HttpMethod.Get)
        {
        }
    }
}
