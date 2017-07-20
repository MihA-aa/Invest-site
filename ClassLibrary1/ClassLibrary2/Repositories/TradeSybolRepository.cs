using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DAL.Entities.Views;
using DAL.Interfaces;
using DALEF.EF;

namespace DALEF.Repositories
{
    public class TradeSybolRepository: ITradeSybolRepository
    {
        private readonly IDbConnection DapperConnection;

        private const string getPriceForDate = "SELECT TOP 1 TradeIndex FROM TradeSybolInformation WHERE SymbolID = @symbolId AND TradeDate <= @date ORDER BY TradeDate DESC";
        private const string getPriceAndDateLastUpdate = "SELECT TOP 1 * FROM TradeSybolInformation WHERE SymbolID = @symbolId AND TradeDate <= GETDATE() ORDER BY TradeDate DESC";
        private const string getMaxDateForGainForSymbol = "SELECT * FROM getMaxMinGainForSymbolInDateInterval(@dateFrom, @dateTo, @symbolId)";
        private const string getDateForSymbolInDateInterval = "SELECT * FROM getPriceDividendForSymbolInDateInterval(@datefrom, @dateto, @symbolId)";

        public TradeSybolRepository(IDbConnection dapperConnection)
        {
            DapperConnection = dapperConnection;
        }

        public decimal GetPriceForDate(DateTime date, int symbolId)
        {
            return DapperConnection.Query<decimal>(getPriceForDate, new { symbolId, date }).SingleOrDefault();
        }

        public TradeSybolView GetPriceAndDateLastUpdate(int symbolId)
        {
            return DapperConnection.Query<TradeSybolView>(getPriceAndDateLastUpdate, new { symbolId}).SingleOrDefault();
        }

        public IEnumerable<TradeInforamation> GetMaxDateForGainForSymbol(DateTime dateFrom, DateTime dateTo, int symbolId)
        {
            string datefrom = dateFrom.ToString("yyyy-MM-dd");
            string dateto = dateTo.ToString("yyyy-MM-dd");
            return DapperConnection.Query<TradeInforamation>(getMaxDateForGainForSymbol, new { datefrom, dateto, symbolId }).ToList();
        }
        public IEnumerable<TradeInforamation> GetDateForSymbolInDateInterval(DateTime dateFrom, DateTime dateTo, int symbolId)
        {
            string datefrom = dateFrom.ToString("yyyy-MM-dd");
            string dateto = dateTo.ToString("yyyy-MM-dd");
            return DapperConnection.Query<TradeInforamation>(getDateForSymbolInDateInterval, new { datefrom, dateto, symbolId }).ToList();
        }
        
    }
}
