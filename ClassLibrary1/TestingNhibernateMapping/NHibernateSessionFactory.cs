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
        private static ISessionFactory _sessionFactory;

        private static readonly Configuration _configuration;

        public static ISession getSession(string connectionString)
        {
            var myEntities = new[] { typeof(UserEntity), typeof(Role) };
            string connectionString2 = "Data Source=ERMOLAEVM;Initial Catalog=FuckingDb; Integrated Security=True;MultipleActiveResultSets=True;";
            ISessionFactory sessionFactory = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString2)
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

        public static Configuration Configuration
        {
            get
            {
                return _configuration;
            }
        }

        public static ISessionFactory SessionFactory
        {
            get
            {
                return _sessionFactory;
            }
        }
    }
}
