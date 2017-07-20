using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DAL.Entities.Views;
using DAL.Interfaces;
using DALEF.EF;

namespace DALEF.Repositories
{
    public class SymbolViewRepository: ISymbolViewRepository
    {
        private IDbConnection DapperConnection;

        private const string getSymbolViewByName = "SELECT * FROM SymbolView WHERE Symbol = @symbol";
        private const string getSymbolViewById = "SELECT * FROM SymbolView WHERE SymbolID = @Id";
        private const string searchSymbolsViewByName = "SELECT DISTINCT TOP(20) Symbol FROM SymbolView WHERE Symbol LIKE @symbol + '%' ORDER BY Symbol";

        public SymbolViewRepository(IDbConnection dapperConnection)
        {
            DapperConnection = dapperConnection;
        }

        public SymbolView Get(int id)
        {
            return DapperConnection.Query<SymbolView>(getSymbolViewById, new { id }).SingleOrDefault();
        }
        
        public IEnumerable<string> SearchSymbolsViewByName(string symbol)
        {
            return DapperConnection.Query<string>(searchSymbolsViewByName, new { symbol }).ToList();
        }
        public SymbolView GetSymbolViewByName(string symbol)
        {
            return DapperConnection.Query<SymbolView>(getSymbolViewByName, new { symbol }).SingleOrDefault();
        }
    }
}
