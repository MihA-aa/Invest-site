using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.WebApi;
using BLL.Infrastructure;
using Microsoft.Owin.Security;

namespace WebApi.Util
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterModule(new ServiceModule(/*"DefaultConnection", "test"*/));
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication)
                .As<IAuthenticationManager>();

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}