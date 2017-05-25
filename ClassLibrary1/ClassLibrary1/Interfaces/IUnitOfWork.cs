using System;
using DAL.ApplicationManager;
using DAL.Entities;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IPortfolioRepository Portfolios { get; }
        IProfileRepository Profiles { get; }
        IPositionRepository Positions { get; }
        ISymbolRepository Symbols { get; }
        ISymbolViewRepository SymbolViews { get; }
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }

        void Save();
        Task SaveAsync();
    }
}
