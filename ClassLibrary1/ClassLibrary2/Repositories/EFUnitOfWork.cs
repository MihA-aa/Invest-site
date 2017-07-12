using System;
using System.Reflection;
using System.Runtime.InteropServices;
//using System.Data.Entity;
using DAL.ApplicationManager;
using DAL.Entities;
using DAL.Interfaces;
//using DALEF.EF;
using NHibernate.AspNet.Identity;
using System.Threading.Tasks;
using DALEF.EF;
using Microsoft.AspNet.Identity;
using NHibernate;
using NHibernate.AspNet.Identity.Helpers;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace DALEF.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        //private ApplicationContext db;
        private DatabaseFirstContext viewDb;
        private SymbolViewRepository SymbolsViews;
        private CustomerRepository customerRepository;
        private PortfolioRepository portfolioRepository;
        private PositionRepository positionRepository;
        private ProfileRepository profileRepository;
        private SymbolDividendRepository symbolDividendRepository;
        private TradeSybolRepository tradeSybolRepository;
        private ViewTemplateRepository viewTemplateRepository;
        private ViewTemplateColumnRepository viewTemplateColumnRepository;
        private ColumnRepository columnRepository;
        private ColumnFormatRepository columnFormatRepository;
        private FormatRepository formatRepository;
        private ViewRepository viewRepository;
        private RecordRepository recordRepository;
        private UserManager<User> userManager;
        private ApplicationRoleManager roleManager;

        private readonly ISessionFactory sessionFactory;
        private ITransaction _transaction;
        public ISession Session { get; private set; }

        public EFUnitOfWork(/*ISessionFactory sessionFactory*//*string connectionString, string connectionStringForExistDB*/)
        {
            viewDb = new DatabaseFirstContext("test");

            //this.sessionFactory = sessionFactory;
            //Session = sessionFactory.OpenSession();

            //sessionFactory = Fluently.Configure()
            //.Database(MsSqlConfiguration.MsSql2008.ConnectionString(x => x.FromConnectionStringWithKey("UnitOfWorkExample")))
            //.Mappings(x => x.AutoMappings.Add(
            //    AutoMap.AssemblyOf<Product>(new AutomappingConfiguration()).UseOverridesFromAssemblyOf<ProductOverrides>()))
            //.ExposeConfiguration(config => new SchemaUpdate(config).Execute(false, true))
            //.BuildSessionFactory();

            Configuration _configuration = new Configuration();
            _configuration
               .Configure()
                .SetNamingStrategy(DefaultNamingStrategy.Instance)
                .SetProperty(NHibernate.Cfg.Environment.UseProxyValidator, "true")
                .DataBaseIntegration(db => {
                    db.ConnectionString = @"Data Source=ERMOLAEVM;Initial Catalog=NewMyDB;Integrated Security=True;MultipleActiveResultSets=True";
                    db.Dialect<MsSql2008Dialect>();
                });


            var mapper = new ModelMapper();
            var myEntities1 = new[] { typeof(Employee) };
            //mapper.AddMappings(myEntities1);
            Type[] sa = new Type[] { typeof(DAL.Entities.Book), typeof(DAL.Entities.BookMap) };
            mapper.AddMappings(sa);
            var T = Assembly.GetExecutingAssembly().GetExportedTypes();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            _configuration.AddMapping(mapping);

            //var myEntities = new[] { typeof(User) };
            //_configuration.AddDeserializedMapping(MappingHelper.GetIdentityMappings(myEntities), null);

            new SchemaUpdate(_configuration).Execute(true, true);
            sessionFactory = _configuration.BuildSessionFactory();

            Session = sessionFactory.OpenSession();
            Session.Save(new DAL.Entities.Book { Id = 1, Name = "12312", Description = "adasd" });
            Session.Save(new Employee { Id = 1, Name = "12312", Description = "adasd" });
        } 

        public UserManager<User> UserManager
        {
            get
            {
                if (userManager == null)
                    userManager = new UserManager<User>(new UserStore<User>(Session));
                return userManager;
            }
        }
        
        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (roleManager == null)
                    roleManager = new ApplicationRoleManager(new RoleStore<Role>(Session));
                return roleManager;
            }
        }

        public IRecordRepository Records
        {
            get
            {
                if (recordRepository == null)
                    recordRepository = new RecordRepository(Session);
                return recordRepository;
            }
        }

        public IViewRepository Views
        {
            get
            {
                if (viewRepository == null)
                    viewRepository = new ViewRepository(Session);
                return viewRepository;
            }
        }

        public IFormatRepository Formats
        {
            get
            {
                if (formatRepository == null)
                    formatRepository = new FormatRepository(Session);
                return formatRepository;
            }
        }

        public IColumnFormatRepository ColumnFormats
        {
            get
            {
                if (columnFormatRepository == null)
                    columnFormatRepository = new ColumnFormatRepository(Session);
                return columnFormatRepository;
            }
        }

        public IColumnRepository Columns
        {
            get
            {
                if (columnRepository == null)
                    columnRepository = new ColumnRepository(Session);
                return columnRepository;
            }
        }

        public IViewTemplateColumnRepository ViewTemplateColumns
        {
            get
            {
                if (viewTemplateColumnRepository == null)
                    viewTemplateColumnRepository = new ViewTemplateColumnRepository(Session);
                return viewTemplateColumnRepository;
            }
        }

        public IViewTemplateRepository ViewTemplates
        {
            get
            {
                if (viewTemplateRepository == null)
                    viewTemplateRepository = new ViewTemplateRepository(Session);
                return viewTemplateRepository;
            }
        }

        public ISymbolDividendRepository SymbolDividends
        {
            get
            {
                if (symbolDividendRepository == null)
                    symbolDividendRepository = new SymbolDividendRepository(viewDb);
                return symbolDividendRepository;
            }
        }

        public ITradeSybolRepository TradeSybols
        {
            get
            {
                if (tradeSybolRepository == null)
                    tradeSybolRepository = new TradeSybolRepository(viewDb);
                return tradeSybolRepository;
            }
        }

        public ISymbolViewRepository SymbolViews
        {
            get
            {
                if (SymbolsViews == null)
                    SymbolsViews = new SymbolViewRepository(viewDb);
                return SymbolsViews;
            }
        }
        
        public ICustomerRepository Customers
        {
            get
            {
                if (customerRepository == null)
                    customerRepository = new CustomerRepository(Session);
                return customerRepository;
            }
        }
        public IPortfolioRepository Portfolios
        {
            get
            {
                if (portfolioRepository == null)
                    portfolioRepository = new PortfolioRepository(Session);
                return portfolioRepository;
            }
        }
        public IProfileRepository Profiles
        {
            get
            {
                if (profileRepository == null)
                    profileRepository = new ProfileRepository(Session);
                return profileRepository;
            }
        }
        public IPositionRepository Positions
        {
            get
            {
                if (positionRepository == null)
                    positionRepository = new PositionRepository(Session);
                return positionRepository;
            }
        }
        public void BeginTransaction()
        {
            _transaction = Session.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                // commit transaction if there is one active
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Commit();
            }
            catch
            {
                // rollback if there was an exception
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();

                throw;
            }
            finally
            {
                Session.Dispose();
            }
        }

        public void Rollback()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();
            }
            finally
            {
                Session.Dispose();
            }
        }
        //public virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //            //userManager.Dispose();
        //            //roleManager.Dispose();
        //        }
        //        this.disposed = true;
        //    }
        //}

        public void Dispose()
        {
            _transaction?.Dispose();
            //Dispose(true);
            //GC.SuppressFinalize(this);
        }
    }
}
