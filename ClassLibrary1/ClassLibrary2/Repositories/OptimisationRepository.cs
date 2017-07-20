using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DAL.Entities;
using DAL.Interfaces;

namespace DALEF.Repositories
{
    public class OptimisationRepository : IOptimisationRepository
    {
        private readonly IDbConnection DapperConnection;

        private const string getPortfolioPosition = "SELECT * FROM Position WHERE Portfolio = @portflio";
        private const string getPortfolio = "SELECT * FROM Portfolio WHERE Id = @portfolioId";

        public OptimisationRepository(IDbConnection dapperConnection)
        {
            DapperConnection = dapperConnection;
        }

        public IEnumerable<Position> GetPortfolioPosition(int portfolioId)
        {
            var portflio =  DapperConnection.Query<Portfolio>(getPortfolio, new { portfolioId }).SingleOrDefault();
            return DapperConnection.Query<Position>(getPortfolioPosition, new { portflio }).ToList();
        }


    }
}
