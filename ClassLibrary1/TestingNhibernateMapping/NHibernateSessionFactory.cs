﻿using System;
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
            //Configuration _configuration = new Configuration();
            //_configuration
            //   .Configure()
            //    .SetNamingStrategy(DefaultNamingStrategy.Instance)
            //    .SetProperty(NHibernate.Cfg.Environment.UseProxyValidator, "true")
            //    .DataBaseIntegration(db => {
            //        db.ConnectionString = @"Data Source=ERMOLAEVM;Initial Catalog=" + connectionString + ";Integrated Security=True;MultipleActiveResultSets=True";
            //        db.Dialect<MsSql2008Dialect>();
            //    });


            //var mapper = new ModelMapper();

            //var assemblyForExportTypes = Assembly.GetExecutingAssembly().GetReferencedAssemblies()
            //    .FirstOrDefault(a => a.FullName == "DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            //var types = Assembly.Load(assemblyForExportTypes).GetExportedTypes();
            //mapper.AddMappings(types);
            //HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            //_configuration.AddMapping(mapping);

            //var myEntities = new[] { typeof(User) };
            //_configuration.AddDeserializedMapping(MappingHelper.GetIdentityMappings(myEntities), null);

            //foreach (PersistentClass persistentClass in _configuration.ClassMappings)
            //{
            //    persistentClass.DynamicUpdate = true;
            //}

            //new SchemaUpdate(_configuration).Execute(true, true);
            //_sessionFactory = _configuration.BuildSessionFactory();

            //return _sessionFactory.OpenSession();

            var myEntities = new[] { typeof(User) };

            ISessionFactory sessionFactory = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2008.ConnectionString(@"Data Source=ERMOLAEVM;Initial Catalog=FuckingDb; Integrated Security=True;MultipleActiveResultSets=True;")
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
