using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using BLL.Infrastructure;
using PL.App_Start;
using PL.Controllers;
using PL.Util;

namespace PL
{
    public class MvcApplication : System.Web.HttpApplication
    {
        internal static MapperConfiguration MapperConfiguration { get; private set; }

        protected void Application_Start()
        {
            AutofacConfig.ConfigureContainer();
            MapperConfiguration = AutoMapperWebConfiguration.GetConfiguration();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            log4net.Config.XmlConfigurator.Configure();
        }
        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;
            Response.Clear();
            Server.ClearError();
            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = "General";
            routeData.Values["exception"] = exception;
            Response.StatusCode = 500;
            if (httpException != null) {
                Response.StatusCode = httpException.GetHttpCode();
                switch (Response.StatusCode) {
                case 403:
                    routeData.Values["action"] = "Http403";
                    break;
                case 404:
                    routeData.Values["action"] = "Http404";
                    break;
                }
            }
            Response.TrySkipIisCustomErrors = true;
            IController errorsController = new ErrorController();
            HttpContextWrapper wrapper = new HttpContextWrapper(Context);
            var rc = new RequestContext(wrapper, routeData);
            errorsController.Execute(rc);
        }
    }
}
