using System;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> Customers { get; }
        IPortfolioRepository Portfolios { get; }
        IRepository<Position> Positions { get; }
        IRepository<Symbol> Symbols { get; }
        IRepository<User> Users { get; }
        IRepository<Dividend> Dividends { get; }
        
        void Save();
    }
}
