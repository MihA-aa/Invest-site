using System;
using System.Data;
//using System.Data.Entity;
using DAL.ApplicationManager;
using DAL.Entities;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

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
        IRecordRepository Records { get; }
        IViewTemplateRepository ViewTemplates { get; }
        IViewTemplateColumnRepository ViewTemplateColumns { get; }
        IFormatRepository Formats { get; }
        IColumnRepository Columns { get; }
        IColumnFormatRepository ColumnFormats { get; }
        IViewRepository Views { get; }
        UserManager<User> UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        void Commit();
        void Rollback();
    }
}
