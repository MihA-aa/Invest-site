using ClassLibrary1.EF;
using ClassLibrary1.Entities;
using ClassLibrary1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;
        private SymbolRepository symbolRepository;
        private CustomerRepository customerRepository;
        private PortfolioRepository portfolioRepository;
        private PositionRepository positionRepository;
        private UserRepository userRepository;
        public EFUnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
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
        public IRepository<Customer> Customers
        {
            get
            {
                if (customerRepository == null)
                    customerRepository = new CustomerRepository(db);
                return customerRepository;
            }
        }
        public IRepository<Portfolio> Portfolios
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
        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
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
