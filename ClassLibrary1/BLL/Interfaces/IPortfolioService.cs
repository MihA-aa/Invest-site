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
        int CreatePortfolio(PortfolioDTO portfolio);
        void DeletePortfolio(int? id);
        void UpdatePortfolio(PortfolioDTO portfolio);
        void UpdatePortfoliosDisplayIndex(Dictionary<string, string> portfolios);
    }
}
