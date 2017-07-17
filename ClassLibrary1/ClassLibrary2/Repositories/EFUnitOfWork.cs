using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
//using System.Data.Entity;
using DAL.ApplicationManager;
using DAL.Entities;
using DAL.Interfaces;
//using DALEF.EF;
using NHibernate.AspNet.Identity;
using System.Threading.Tasks;
using DAL.Enums;
using DALEF.EF;
using DALEF.NHibernateHelp;
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
        private UserManager<UserEntity> userManager;
        private ApplicationRoleManager roleManager;
        private readonly string connectionString;

        private ITransaction _transaction;
        public ISession Session { get; private set; }

        public EFUnitOfWork(string connectionString, string connectionStringForExistDB)
        {
            this.connectionString = connectionString;
            viewDb = new DatabaseFirstContext(connectionStringForExistDB);
            Session = NHibernateSessionFactory.getSession(connectionString);
            //StoreDbInitializer.Inizialize(Session);
        }

        public UserManager<UserEntity> UserManager
        {
            get
            {
                if (userManager == null)
                    userManager = new UserManager<UserEntity>(new UserStore<UserEntity>(Session));
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

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if(!Session.IsOpen)
                Session = NHibernateSessionFactory.getSession(connectionString);
            _transaction = Session.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                {
                    _transaction.Commit();
                    _transaction.Dispose();
                }
            }
            catch
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();
                throw;
            }
            finally
            {
                //Session.Close();
                Session.Dispose();
            }
        }

        public void Save()
        {
            Session.Flush();
        }

        public bool SessionIsOpen()
        {
            return Session.IsOpen;
        }
        
        public void RollBack()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                {
                    _transaction.Rollback();
                    _transaction.Dispose();
                }
            }
            finally
            {
                //Session.Close();
                Session.Dispose();
            }
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this._transaction != null)
                    {
                        this._transaction.Dispose();
                        this._transaction = null;
                    }

                    if (this.Session != null)
                    {
                        this.Session.Dispose();
                        Session = null;
                    }
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
