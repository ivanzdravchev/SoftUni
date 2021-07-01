using MyWebServer.Server.Http;
using MyWebServer.Server.Identity;
using MyWebServer.Server.Results;
using MyWebServer.Server.Results.Views;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MyWebServer.Server.Controllers
{
    public abstract class Controller
    {
        public const string UserSessionKey = "AuthenticatedUserId";

        private UserIdentity userIdentity;
        private IViewEngine viewEngine;

        protected HttpRequest Request { get; init; }

        protected HttpResponse Response { get; private init; } = new HttpResponse(HttpStatusCode.OK);

        protected UserIdentity User
        {
            get
            {
                if (this.userIdentity == null)
                {
                    this.userIdentity = this.Request.Session.Contains(UserSessionKey)
                        ? new UserIdentity { Id = this.Request.Session[UserSessionKey] }
                        : new UserIdentity();
                }

                return this.userIdentity;
            }
        }

        protected IViewEngine ViewEngine
        {
            get
            {
                if (this.viewEngine == null)
                {
                    this.viewEngine = this.Request.Services.Get<IViewEngine>()
                        ?? new ParserViewEngine();
                }

                return this.viewEngine;
            }
        }

        protected void SignIn(string userId)
        {
            this.Request.Session[UserSessionKey] = userId;

            this.userIdentity = new UserIdentity { Id = userId };
        }

        protected void SignOut()
        {
            this.Request.Session.Remove(UserSessionKey);

            this.userIdentity = new UserIdentity();
        }

        protected ActionResult Text(string text)
        {
            return new TextResult(this.Response, text);
        }

        protected ActionResult Html(string html)
        {
            return new HtmlResult(this.Response, html);
        }

        protected ActionResult BadRequest()
        {
            return new BadRequestResult(this.Response);
        }

        protected ActionResult Unauthorized()
        {
            return new UnauthorizedResult(this.Response);
        }

        protected ActionResult Redirect(string location)
        {
            return new RedirectResult(this.Response, location);
        }

        protected ActionResult View([CallerMemberName] string viewName = "")
        {
            return this.GetViewResult(viewName, null);
        }

        protected ActionResult View(string viewName, object model)
        {
            return this.GetViewResult(viewName, model);
        }

        protected ActionResult View(object model, [CallerMemberName] string viewName = "")
        {
            return this.GetViewResult(viewName, model);
        }

        protected ActionResult Error(string error)
        {
            return this.Error(new[] { error });
        }

        protected ActionResult Error(IEnumerable<string> errors)
        {
            return this.View("./Error", errors);
        }

        private ActionResult GetViewResult(string viewName, object model)
        {
            return new ViewResult(this.Response, this.ViewEngine, viewName, this.GetType().GetControllerName(), model, this.User.Id);
        }
    }
}
