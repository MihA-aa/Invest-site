using DAL.Entities;
using DAL.Interfaces;
using DALEF.EF;

namespace DALEF.Repositories
{
    public class PortfolioRepository : GenericRepository<Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(ApplicationContext context) : base(context)
        {
        }

        public void AddPositionToPortfolio(Position position, int portfolioId)
        {
            var portfolio = dbSet.Find(portfolioId);
            portfolio.Positions.Add(position);
            position.Portfolio = portfolio;
        }
    }
}
