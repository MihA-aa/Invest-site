using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DAL.Entities.Views;
using DAL.Interfaces;
using DALEF.EF;

namespace DALEF.Repositories
{
    public class SymbolDividendRepository : ISymbolDividendRepository
    {
        private IDbConnection DapperConnection;

        private const string getSymbolDividendById = "SELECT * FROM SymbolView WHERE SymbolID = @id";
        private const string getDividendsInDateInterval = "SELECT SUM(DividendAmount) FROM SymbolDividends WHERE SymbolID = @symbolId AND TradeDate >= @datefrom  AND TradeDate <= @dateto";
        public SymbolDividendRepository(IDbConnection dapperConnection)
        {
            DapperConnection = dapperConnection;
        }

        public SymbolDividend Get(int id)
        {
            return DapperConnection.Query<SymbolDividend>(getSymbolDividendById, new { id }).SingleOrDefault();
        }

        public decimal GetDividendsInDateInterval(DateTime dateFrom, DateTime dateTo, int symbolId)
        {
            string datefrom = dateFrom.ToString("yyyy-MM-dd");
            string dateto = dateTo.ToString("yyyy-MM-dd");
            var we =  DapperConnection.Query<decimal?>(getDividendsInDateInterval, new { symbolId, datefrom, dateto }).SingleOrDefault() ?? 0;
            return we;
        }
    }
}