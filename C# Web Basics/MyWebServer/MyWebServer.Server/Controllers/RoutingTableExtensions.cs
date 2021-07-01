using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MyWebServer.Server.Http;
using MyWebServer.Server.Routing;

namespace MyWebServer.Server.Controllers
{
    public static class RoutingTableExtensions
    {
        private static Type stringType = typeof(string);
        private static Type httpResponseType = typeof(HttpResponse);

        public static IRoutingTable MapGet<TController>(
            this IRoutingTable routingTable,
            string path,
            Func<TController, HttpResponse> controllerFunction)
            where TController : Controller
        {
            return routingTable.MapGet(path, request =>
            {
                return controllerFunction(CreateController<TController>(request));
            });
        }

        public static IRoutingTable MapPost<TController>(
            this IRoutingTable routingTable,
            string path,
            Func<TController, HttpResponse> controllerFunction)
            where TController : Controller
        {
            return routingTable.MapPost(path, request =>
            {
                return controllerFunction(CreateController<TController>(request));
            });
        }

        public static IRoutingTable MapControllers(this IRoutingTable routingTable)
        {
            var controllerActions = GetControllerActions();

            foreach (var controllerAction in controllerActions)
            {
                var controllerName = controllerAction.DeclaringType.GetControllerName();
                var actionName = controllerAction.Name;

                var path = $"/{controllerName}/{actionName}";

                var responseFunction = GetResponseFunction(controllerAction);

                var httpMethod = HttpMethod.Get;

                var httpMethodAttribute = controllerAction.GetCustomAttribute<HttpMethodAttribute>();

                if (httpMethodAttribute != null)
                {
                    httpMethod = httpMethodAttribute.HttpMethod;
                }

                routingTable.Map(httpMethod, path, responseFunction);

                // MapDefaultRoutes
                if (actionName == "Index")
                {
                    routingTable.Map(httpMethod, $"/{controllerName}", responseFunction);

                    if (controllerName == "Home")
                    {
                        routingTable.Map(httpMethod, "/", responseFunction);
                    }
                }
            }

            return routingTable;
        }

        private static Func<HttpRequest, HttpResponse> GetResponseFunction(MethodInfo controllerAction)
        {
            return request =>
            {
                if (!IsUserAuthorized(controllerAction, request.Session))
                {
                    return new HttpResponse(HttpStatusCode.Unauthorized);
                }

                var controllerInstance = CreateController(controllerAction.DeclaringType, request);

                var parameterValues = GetParameterValues(controllerAction, request);

                return (HttpResponse)controllerAction.Invoke(controllerInstance, parameterValues);
            };
        }

        private static IEnumerable<MethodInfo> GetControllerActions()
        {
            return Assembly
                .GetEntryAssembly()
                .GetExportedTypes()
                .Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(Controller)) && t.Name.EndsWith(nameof(Controller)))
                .SelectMany(t => t
                    .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(m => m.ReturnType.IsAssignableTo(httpResponseType)))
                .ToList();
        }

        private static TController CreateController<TController>(HttpRequest request)
            where TController : Controller
        {
            return (TController)CreateController(typeof(TController), request);
        }

        private static Controller CreateController(Type controllerType, HttpRequest request)
        {
            var controller = (Controller)request.Services.CreateInstance(controllerType);

            controllerType
                .GetProperty("Request", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(controller, request);

            return controller;
        }

        private static bool IsUserAuthorized(MethodInfo controllerAction, HttpSession session)
        {
            var authorizationRequired = controllerAction.DeclaringType.GetCustomAttribute<AuthorizeAttribute>()
                    ?? controllerAction.GetCustomAttribute<AuthorizeAttribute>();

            if (authorizationRequired != null)
            {
                var userIsAuthorized = session.Contains(Controller.UserSessionKey)
                    && session[Controller.UserSessionKey] != null;

                if (!userIsAuthorized)
                {
                    return false;
                }
            }

            return true;
        }

        private static object[] GetParameterValues(MethodInfo controllerAction, HttpRequest request)
        {
            var actionParameters = controllerAction
                .GetParameters()
                .Select(p => new
                {
                    Name = p.Name,
                    Type = p.ParameterType
                })
                .ToArray();

            var parameterValues = new object[actionParameters.Length];

            for (int i = 0; i < actionParameters.Length; i++)
            {
                var parameter = actionParameters[i];
                var parameterName = parameter.Name;
                var parameterType = parameter.Type;

                if (parameterType.IsPrimitive || parameterType == stringType)
                {
                    var parameterValue = request.GetValue(parameterName);

                    parameterValues[i] = Convert.ChangeType(parameterValue, parameterType);
                }
                else
                {
                    var parameterValue = Activator.CreateInstance(parameterType);

                    var parameterProperties = parameterType.GetProperties();

                    foreach (var property in parameterProperties)
                    {
                        var propertyValue = request.GetValue(property.Name);

                        property.SetValue(parameterValue, Convert.ChangeType(propertyValue, property.PropertyType));
                    }

                    parameterValues[i] = parameterValue;
                }
            }

            return parameterValues;
        }

        private static string GetValue(this HttpRequest request, string name)
        {
            return request.Query.GetValueOrDefault(name)
                ?? request.Form.GetValueOrDefault(name);
        }

        private static object ConvertType(Type type, object value)
        {
            return type == stringType
                ? value
                : Convert.ChangeType(value, type);
        }
    }
}
