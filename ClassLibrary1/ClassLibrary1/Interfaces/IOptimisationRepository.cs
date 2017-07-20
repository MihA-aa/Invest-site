using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IOptimisationRepository
    {
        IEnumerable<Position> GetPortfolioPosition(int portfolioId);
    }
}
