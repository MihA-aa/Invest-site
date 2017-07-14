using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DAL.Entities;
using DAL.Interfaces;
using NHibernate;
using NHibernate.Linq;

namespace DALEF.Repositories
{
    public class PortfolioRepository : GenericRepository<Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(ISession session) : base(session)
        {
        }

        public void AddPositionToPortfolio(Position position, int portfolioId)
        {
            var portfolio = Session.Get<Portfolio>(portfolioId);
            portfolio.Positions.Add(position);
            position.Portfolio = portfolio;
            Session.Flush();
        }

        public void ChangePortfolioDisplayIndex(int id, int displayIndex)
        {
            Portfolio portfolio = Session.Get<Portfolio>(id);
            portfolio.DisplayIndex = displayIndex;
            Session.Update(portfolio);

            //var portfolio = new Portfolio {Id = id, DisplayIndex = displayIndex};
            //dbSet.Attach(portfolio);
            //db.Entry(portfolio).Property(x => x.DisplayIndex).IsModified = true;
            //db.SaveChanges();
        }

        public void RecalculatePortfolioValue(int id)
        {
            Portfolio portfolio = Session.Get<Portfolio>(id);
            portfolio.PortfolioValue = portfolio.Positions.Sum(p => p.AbsoluteGain);
            portfolio.Quantity = portfolio.Positions.Count();
            portfolio.LastUpdateDate = DateTime.Now;
            portfolio.PercentWins = GetPercentWins(id);
            portfolio.BiggestWinner = portfolio.Positions.DefaultIfEmpty().Max(p => p?.Gain ?? 0);
            portfolio.BiggestLoser = portfolio.Positions.DefaultIfEmpty().Min(p => p?.Gain ?? 0);
            portfolio.AvgGain = portfolio.Positions.DefaultIfEmpty().Average(p => p?.Gain ?? 0);
            portfolio.MonthAvgGain = portfolio.Positions
                .Where(p => p.OpenDate.Month <= DateTime.Today.Month && p.OpenDate.Year <= DateTime.Today.Year)
                .Where(p => p.CloseDate == null || p.CloseDate.Value.Month >= DateTime.Today.Month && p.CloseDate.Value.Year >= DateTime.Today.Year)
                .DefaultIfEmpty()
                .Average(p => p == null ? 0 : p.Gain);
            Update(portfolio);
        }

        public decimal GetPercentWins(int id)
        {
            var portfolio = Session.Get<Portfolio>(id);
            var winValuecount = portfolio.Positions.Count(pos => pos.Gain > 0);
            var allCount = portfolio.Positions.Count();
            return allCount == 0 ? 0 : winValuecount/allCount;
        }

        public void UpdatePortfolioNameAndNotes(Portfolio portfolio)
        {
            Portfolio portfolioForUpdate = Session.Get<Portfolio>(portfolio.Id);
            portfolioForUpdate.Name = portfolio.Name;
            portfolioForUpdate.Notes = portfolio.Notes;
            Session.Update(portfolioForUpdate);
        }

        public bool IsExist(int id)
        {
            return Session.Query<Portfolio>()
                .Any(p => p.Id == id);
        }

        public IQueryable<Portfolio> GetPortfolioQuery(int id)
        {
            return Session.Query<Portfolio>().Where(p=>p.Id == id);
        }
    }
}
