using MyWebServer.Server.Http;

namespace MyWebServer.Server.Results
{
    public class UnauthorizedResult : ActionResult
    {
        public UnauthorizedResult(HttpResponse response)
            : base(response)
        {
            this.StatusCode = HttpStatusCode.Unauthorized;
        }
    }
}
