using System;

namespace MyWebServer.Server.Controllers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthorizeAttribute : Attribute
    {
    }
}
