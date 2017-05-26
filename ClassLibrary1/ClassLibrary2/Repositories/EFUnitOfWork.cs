using System;
using DAL.ApplicationManager;
using DAL.Entities;
using DAL.Interfaces;
using DALEF.EF;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace DALEF.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;
        private MyExistingDatabaseContext  viewDb;
        private SymbolViewRepository SymbolsViews;
        private CustomerRepository customerRepository;
        private PortfolioRepository portfolioRepository;
        private PositionRepository positionRepository;
        private ProfileRepository profileRepository;
        private TradeSybolRepository tradeSybolRepository;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        public EFUnitOfWork(string connectionString, string connectionStringForExistDB)
        {
            db = new ApplicationContext(connectionString);
            viewDb = new MyExistingDatabaseContext(connectionStringForExistDB);
        } 

        public ApplicationUserManager UserManager
        {
            get
            {
                if (userManager == null)
                    userManager = new ApplicationUserManager(new UserStore<User>(db));
                return userManager;
            }
        }
        
        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (roleManager == null)
                    roleManager = new ApplicationRoleManager(new RoleStore<Role>(db));
                return roleManager;
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
                    customerRepository = new CustomerRepository(db);
                return customerRepository;
            }
        }
        public IPortfolioRepository Portfolios
        {
            get
            {
                if (portfolioRepository == null)
                    portfolioRepository = new PortfolioRepository(db);
                return portfolioRepository;
            }
        }
        public IProfileRepository Profiles
        {
            get
            {
                if (profileRepository == null)
                    profileRepository = new ProfileRepository(db);
                return profileRepository;
            }
        }
        public IPositionRepository Positions
        {
            get
            {
                if (positionRepository == null)
                    positionRepository = new PositionRepository(db);
                return positionRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }
        public async Task SaveAsync() 
        {
            await db.SaveChangesAsync();
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                    //userManager.Dispose();
                    //roleManager.Dispose();
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
