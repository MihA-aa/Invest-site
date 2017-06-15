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
        protected DatabaseFirstContext db;
        protected DbSet<SymbolDividend> dbSet;
        public SymbolDividendRepository(DatabaseFirstContext context)
        {
            db = context;
            dbSet = context.Set<SymbolDividend>();
        }

        public SymbolDividend Get(int id)
        {
            return dbSet.Find(id);
        }

        public decimal GetDividendsInDateInterval(DateTime dateFrom, DateTime dateTo, int symbolId)
        {
            return dbSet
                .Where(a =>
                    DbFunctions.TruncateTime(a.TradeDate) >= dateFrom.Date &&
                    DbFunctions.TruncateTime(a.TradeDate) <= dateTo.Date &&
                    a.SymbolID == symbolId)
                .Sum(x => (decimal?)(x.DividendAmount)) ?? 0;
        }
    }
}
