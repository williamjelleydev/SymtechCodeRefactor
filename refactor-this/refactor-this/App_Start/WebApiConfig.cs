using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_this
{
    public class WebApiConfig
    {
        // This is niggly _as_ since I'm not familiar with this... lol.
        // These are things I'm going to have to remove...
        // FUCK - this is more than I bargained for to be honest...
        // Is it worth just _not_ doing this .netcore upgrade after all...
        // This is too much work isn't it. I'm going to stop trying to charge ahead with this upgrade, and just do the minimum code refactoring..

        // Can I just get rid of this completely.. lol
        //public static void Register(HttpConfiguration config)
        //{
        //    // Web API configuration and services
        //    var formatters = GlobalConfiguration.Configuration.Formatters;
        //    formatters.Remove(formatters.XmlFormatter);
        //    formatters.JsonFormatter.Indent = true;

        //    // Web API routes
        //    config.MapHttpAttributeRoutes();

        //    config.Routes.MapHttpRoute(
        //        name: "DefaultApi",
        //        routeTemplate: "api/{controller}/{id}",
        //        defaults: new
        //        {
        //            id = RouteParameter.Optional
        //        }
        //    );
        //}
    }
}