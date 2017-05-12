using DAL.Entities;
using DAL.Interfaces;
using DALEF.EF;

namespace DALEF.Repositories
{
    class SymbolRepository : GenericRepository<Symbol>, ISymbolRepository
    {
        public SymbolRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
