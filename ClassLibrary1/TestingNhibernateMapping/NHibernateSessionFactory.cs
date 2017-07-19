using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using NHibernate;
using NHibernate.AspNet.Identity.Helpers;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;

namespace TestingNhibernateMapping
{

    public class NHibernateSessionFactory
    {
        public static ISession getSession(string connectionString)
        {
            var myEntities = new[] { typeof(UserEntity), typeof(Role) };
            ISessionFactory sessionFactory = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString)
            .ShowSql())
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Position>())
            .ExposeConfiguration(cfg =>
            {
                cfg.AddDeserializedMapping(
                       MappingHelper.GetIdentityMappings(myEntities), null);
                new SchemaUpdate(cfg).Execute(false, true);
            })
            .BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}
