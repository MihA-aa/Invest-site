using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<string> SearchSymbolsByName(string name)
        {
            var symbols = dbSet
                .Where(a => a.Name.StartsWith(name))
                .Select(a => a.Name)
                .Distinct()
                .ToList();
            return symbols;
        }
    }
}
