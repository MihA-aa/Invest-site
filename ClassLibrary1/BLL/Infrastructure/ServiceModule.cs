using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
using AutoMapper;
using BLL.Interfaces;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using DALEF.Repositories;
using Microsoft.AspNet.Identity;
using NHibernate;
using NHibernate.AspNet.Identity;

namespace BLL.Infrastructure
{
    public class ServiceModule : Module
    {
        private readonly string _connectionString;
        public ServiceModule(string connection)
        {
            _connectionString = connection;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>()
                .WithParameters(new List<Parameter> { new NamedParameter("connectionString", _connectionString)}).
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
            builder.RegisterType<SymbolViewService>().As<ISymbolViewService>().
                InstancePerRequest();
            builder.RegisterType<TradeSybolService>().As<ITradeSybolService>().
                InstancePerRequest();
            builder.RegisterType<CustomerService>().As<ICustomerService>().
                InstancePerRequest();
            builder.RegisterType<ViewTemplateService>().As<IViewTemplateService>().
                InstancePerRequest();
            builder.RegisterType<ViewTemplateColumnService>().As<IViewTemplateColumnService>().
                InstancePerRequest();
            builder.RegisterType<ViewService>().As<IViewService>().
                InstancePerRequest();
            builder.RegisterType<ColumnService>().As<IColumnService>().
                InstancePerRequest();
            builder.RegisterType<ProfileService>().As<IProfileService>().
                InstancePerRequest();
            builder.RegisterType<RecordService>().As<IRecordService>().
                InstancePerRequest();
            builder.RegisterType<TransactionService>().As<ITransactionService>().
                InstancePerRequest();
            builder.Register(_ => new AutoMapperConfiguration().Configure().CreateMapper())
                .As<IMapper>().SingleInstance();
            base.Load(builder);
        }
    }
    
}
