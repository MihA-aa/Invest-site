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
using NHibernate.Tool.hbm2ddl;

namespace BLL.Services
{
    public static class NHibernateSessionFactory
    {
        private static readonly ISessionFactory _sessionFactory;

        private static readonly Configuration _configuration;

        static NHibernateSessionFactory()
        {
            _configuration = new Configuration();

            _configuration
               .Configure()
                .SetNamingStrategy(DefaultNamingStrategy.Instance)
                .SetProperty(NHibernate.Cfg.Environment.UseProxyValidator, "true")
                .DataBaseIntegration(db => {
                    db.ConnectionString = @"Data Source=ERMOLAEVM;Initial Catalog=NewMyDB;Integrated Security=True;MultipleActiveResultSets=True";
                    db.Dialect<MsSql2008Dialect>();
                });
            
            var mapper = new ModelMapper();
            var e = Assembly.GetExecutingAssembly();
            var myEntities1 = new[] { typeof(Book) };
            mapper.AddMappings(myEntities1);
            //var t = typeof(Position).Assembly.GetExportedTypes();
            // mapper.AddMappings(typeof(DAL).Assembly.GetTypes());
            // mapper.AddMappings(typeof(Position).Assembly.GetExportedTypes());
            // mapper.AddMappings(new List<Type> { typeof(Profile) });

            //mapper.AddMappings(typeof(Profile).Assembly.GetTypes());
            //mapper.AddMappings(Assembly.GetAssembly(typeof (Position)).GetTypes());

            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            _configuration.AddMapping(mapping);

            var myEntities = new[] { typeof(User) };
            _configuration.AddDeserializedMapping(MappingHelper.GetIdentityMappings(myEntities), null);

            _sessionFactory = _configuration.BuildSessionFactory();

            new SchemaUpdate(_configuration).Execute(true, true);


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
