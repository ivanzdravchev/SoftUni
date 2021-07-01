using System;
using MyWebServer.Server.Http;

namespace MyWebServer.Server.Controllers
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class HttpMethodAttribute : Attribute
    {
        public HttpMethod HttpMethod { get; }

        protected HttpMethodAttribute(HttpMethod httpMethod)
        {
            this.HttpMethod = httpMethod;
        }
    }
}
