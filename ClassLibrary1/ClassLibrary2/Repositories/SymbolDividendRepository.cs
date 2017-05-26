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
    public class SymbolDividendRepository: ISymbolDividendRepository
    {
        protected MyExistingDatabaseContext db;
        protected DbSet<SymbolDividend> dbSet;
        public SymbolDividendRepository(MyExistingDatabaseContext context)
        {
            db = context;
            dbSet = context.Set<SymbolDividend>();
        }

        public SymbolDividend Get(int id)
        {
            return dbSet.Find(id);
        }
    }
}
