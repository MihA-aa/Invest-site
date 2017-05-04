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
        PositionDTO GetPosition(int? id);
        PortfolioDTO GetPortfolio(int? id);
        IEnumerable<PositionDTO> GetPositions();
        IEnumerable<PortfolioDTO> GetPortfolios();
        IEnumerable<PositionDTO> GetPortfolioPositions(int? portfolioId);
    }
}
