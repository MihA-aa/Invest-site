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

        public void ChangePortfolioDisplayIndex(int id, int displayIndex)
        {
            var portfolio = new Portfolio { Id = id, DisplayIndex = displayIndex };
            dbSet.Attach(portfolio);
            db.Entry(portfolio).Property(x => x.DisplayIndex).IsModified = true;
            db.SaveChanges();
        }
    }
}
