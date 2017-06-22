using System;
using System.Data.Entity;
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
        ISymbolViewRepository SymbolViews { get; }
        ITradeSybolRepository TradeSybols { get; }
        ISymbolDividendRepository SymbolDividends { get; }
        IViewTemplateRepository ViewTemplates { get; }
        IViewTemplateColumnRepository ViewTemplateColumns { get; }
        IFormatRepository Formats { get; }
        IColumnRepository Columns { get; }
        IColumnFormatRepository ColumnFormats { get; }
        IViewRepository Views { get; }
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }

        void Save();
        Task SaveAsync();
        DbContextTransaction BeginTransaction();
        void Commit(DbContextTransaction transaction);
        void RollBack(DbContextTransaction transaction);
    }
}
