using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
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
        private string connectionStringForExistDB;
        public ServiceModule(string connection, string connectionForExistDB)
        {
            connectionString = connection;
            connectionStringForExistDB = connectionForExistDB;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>()
                .WithParameters(new List<Parameter> { new NamedParameter("connectionString", connectionString),
            new NamedParameter("connectionStringForExistDB", connectionStringForExistDB) }).
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
            builder.RegisterType<SymbolService>().As<ISymbolService>().
                InstancePerRequest();
            builder.RegisterType<SymbolViewService>().As<ISymbolViewService>().
                InstancePerRequest();
            base.Load(builder);
        }
    }
}
