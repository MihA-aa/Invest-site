using System;
using DAL.ApplicationManager;
using DAL.Entities;
using DAL.Interfaces;
using DALEF.EF;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DALEF.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;
        private SymbolRepository symbolRepository;
        private CustomerRepository customerRepository;
        private PortfolioRepository portfolioRepository;
        private PositionRepository positionRepository;
        private DividendRepository dividendRepository;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        public EFUnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
        }
        //public EFUnitOfWork()
        //{
        //    db = new ApplicationContext();
        //}
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

        public IRepository<Dividend> Dividends
        {
            get
            {
                if (dividendRepository == null)
                    dividendRepository = new DividendRepository(db);
                return dividendRepository;
            }
        }
        public IRepository<Symbol> Symbols
        {
            get
            {
                if (symbolRepository == null)
                    symbolRepository = new SymbolRepository(db);
                return symbolRepository;
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
        public IRepository<Position> Positions
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
