using System;
using DAL.ApplicationManager;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> Customers { get; }
        IPortfolioRepository Portfolios { get; }
        IRepository<Position> Positions { get; }
        IRepository<Symbol> Symbols { get; }
        IRepository<Dividend> Dividends { get; }
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }

        void Save();
    }
}
