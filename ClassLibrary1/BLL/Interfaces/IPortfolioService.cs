using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPortfolioService
    {
        PortfolioDTO GetPortfolio(int? id);
        IEnumerable<PortfolioDTO> GetPortfolios();
        IEnumerable<PositionDTO> GetPortfolioPositions(int? portfolioId);
        IEnumerable<PositionDTO> GetPortfolioPositionsForUser(int? portfolioId, string id);
        int CreateOrUpdatePortfolio(PortfolioDTO portfolio, string userId);
        int CreatePortfolio(PortfolioDTO portfolioDto, string userId);
        void DeletePortfolio(int? id);
        void UpdatePortfolio(PortfolioDTO portfolioDto);
        void UpdatePortfolio(int? id);
        void UpdatePortfoliosDisplayIndex(Dictionary<string, string> portfolios);
        void UpdatePortfolioNameAndNotes(PortfolioDTO portfolioDto);
        void RecalculatePortfolioValue(int? id);
        IEnumerable<PortfolioDTO> GetPortfoliosForUser(string id);
        bool CheckAccess(string userId, int? portfolioId);
    }
}
