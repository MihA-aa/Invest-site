using ClassLibrary1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> Customers { get; }
        IRepository<Portfolio> Portfolios { get; }
        IRepository<Position> Positions { get; }
        IRepository<Symbol> Symbols { get; }
        IRepository<User> Users { get; }

        void Save();
    }
}
