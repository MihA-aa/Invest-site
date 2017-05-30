﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Views;
using DAL.Interfaces;
using DALEF.EF;

namespace DALEF.Repositories
{
    public class TradeSybolRepository: ITradeSybolRepository
    {
        protected MyExistingDatabaseContext db;
        protected DbSet<TradeSybolView> dbSet;
        public TradeSybolRepository(MyExistingDatabaseContext context)
        {
            db = context;
            dbSet = context.Set<TradeSybolView>();
        }

        public decimal GetPriceForDate(DateTime date, int symbolId)
        {
            return dbSet
                .Where(a => DbFunctions.TruncateTime(a.TradeDate) <= date.Date)
                .Where(a => a.SymbolID == symbolId)
                .OrderByDescending(a => a.TradeDate)
                .Select(a => a.TradeIndex)
                .FirstOrDefault();
        }

        public IEnumerable<TradeInforamation> GetMaxDateForGainForSymbol(DateTime dateFrom, DateTime dateTo, int symbolId)
        {
            var myQuery = "SELECT * FROM dbo.[getMaxMinGainForSymbolInDateInterval] ('"+ dateFrom + "', '"+ dateTo + "', "+ symbolId + ")";
            return db.Database.SqlQuery<TradeInforamation>(myQuery).ToList();
        }
    }
}
