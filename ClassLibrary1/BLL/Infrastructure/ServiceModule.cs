using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using DALEF.Repositories;

namespace BLL.Infrastructure
{
    public class ServiceModule : Module
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>().
                WithParameter("connectionString", connectionString).
                InstancePerRequest();
            builder.RegisterType<PortfolioService>().As<IPortfolioService>().
                InstancePerRequest();
            builder.RegisterType<PositionService>().As<IPositionService>().
                InstancePerRequest();
            builder.RegisterType<ValidateService>().As<IValidateService>().
                InstancePerRequest();
            builder.RegisterType<CalculationService>().As<ICalculationService>().
                InstancePerRequest();
            builder.RegisterType<UserService>().As<IUserService>().
                InstancePerRequest();
            base.Load(builder);
        }
    }
}
