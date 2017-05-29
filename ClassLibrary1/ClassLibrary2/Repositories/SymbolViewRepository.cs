using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Views;
using DAL.Interfaces;
using DALEF.EF;

namespace DALEF.Repositories
{
    public class SymbolViewRepository: ISymbolViewRepository
    {
        protected MyExistingDatabaseContext db;
        protected DbSet<SymbolView> dbSet;
        public SymbolViewRepository(MyExistingDatabaseContext context)
        {
            db = context;
            dbSet = context.Set<SymbolView>();
        }
        public IEnumerable<SymbolView> Find(Func<SymbolView, bool> predicate)
        {
            return db.SymbolViews.Where(predicate).ToList();
        }

        public SymbolView Get(int id)
        {
            return dbSet.Find(id);
        }

        public async Task<SymbolView> GetAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }
        
        public IEnumerable<string> SearchSymbolsViewByName(string name)
        {
            return dbSet
                .Where(a => a.Symbol.StartsWith(name))
                .Select(a => a.Symbol)
                .Distinct()
                .Take(20)
                .ToList();
        }
        public SymbolView GetSymbolViewByName(string name)
        {
            var symbolView = dbSet.FirstOrDefault(a => a.Symbol == name);
            return symbolView;
        }
    }
}
