using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DALEF.Repositories;

namespace BLL.Infrastructure
{
    class ServiceModule: Module
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>().
                WithParameter("DefaultConnection", connectionString).
                InstancePerRequest();

            base.Load(builder);
        }
    }
}
