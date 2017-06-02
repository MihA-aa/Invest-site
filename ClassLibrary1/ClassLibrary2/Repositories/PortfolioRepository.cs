using System;
using System.Data.Entity;
using System.Linq;
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
            var portfolio = new Portfolio {Id = id, DisplayIndex = displayIndex};
            dbSet.Attach(portfolio);
            db.Entry(portfolio).Property(x => x.DisplayIndex).IsModified = true;
            db.SaveChanges();
        }

        public void RecalculatePortfolioValue(int id)
        {
            var portfolio = dbSet.Find(id);
            portfolio.PortfolioValue = portfolio.Positions.Sum(p => p.AbsoluteGain);
            portfolio.Quantity = portfolio.Positions.Count();
            portfolio.LastUpdateDate = DateTime.Now;
            portfolio.PercentWins = GetPercentWins(id);
            portfolio.BiggestWinner =  portfolio.Positions.Max(p => p.Gain);
            portfolio.BiggestLoser = portfolio.Positions.Min(p => p.Gain);
            portfolio.AvgGain = portfolio.Positions.Average(p => p.Gain);
            portfolio.MonthAvgGain = portfolio.Positions
                .Where(p => p.OpenDate.Month <= DateTime.Today.Month && p.OpenDate.Year <= DateTime.Today.Year)
                .Where(p => p.CloseDate == null || p.CloseDate.Value.Month >= DateTime.Today.Month && p.CloseDate.Value.Year >= DateTime.Today.Year)
                .Average(p => p.Gain);
            Update(portfolio);
            db.SaveChanges();
        }

        public decimal GetPercentWins(int id)
        {
            var portfolio = dbSet.Find(id);
            var winValuecount = portfolio.Positions.Count(pos => pos.Gain > 0);
            var allCount = portfolio.Positions.Count();
            return allCount == 0 ? 0 : winValuecount/allCount;
        }

        public void UpdatePortfolioNameAndNotes(Portfolio portfolio)
        {
            dbSet.Attach(portfolio);
            db.Entry(portfolio).Property(x => x.Name).IsModified = true;
            db.Entry(portfolio).Property(x => x.Notes).IsModified = true;
            db.Entry(portfolio).Property(x => x.Visibility).IsModified = true;
        }

        public bool IsExist(int id)
        {
            return dbSet
                .AsNoTracking()
                .Any(p => p.Id == id);
        }
    }
}
