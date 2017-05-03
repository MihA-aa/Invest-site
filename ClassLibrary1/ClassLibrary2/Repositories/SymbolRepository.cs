using DAL.Entities;
using DALEF.EF;

namespace DALEF.Repositories
{
    class SymbolRepository : GenericRepository<Symbol>
    {
        public SymbolRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
