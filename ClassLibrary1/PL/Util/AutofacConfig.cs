﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.Owin.Security;

namespace PL.Util
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule(new ServiceModule("DefaultConnection", "test"));
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication)
                .As<IAuthenticationManager>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}