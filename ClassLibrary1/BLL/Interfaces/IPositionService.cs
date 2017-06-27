using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;
using DotNet.Highcharts;

namespace BLL.Interfaces
{
    public interface IPositionService
    {
        PositionDTO GetPosition(int? id);
        IEnumerable<PositionDTO> GetPositions();
        IEnumerable<PositionDTO> GetPositionsForUser(string id);
        Highcharts GetChartForPosition(int? id);
        bool CheckAccess(string userId, int? portfolioId);
        void CreatePosition(PositionDTO position, int? portfolioId);
        void DeletePosition(int? id);
        void UpdatePosition(PositionDTO position);
        void UpdateOnlyPosition(int? id);
        void CreateOrUpdatePosition(PositionDTO position, int? portfolioId);
        void AddPositionToPortfolio(Position position, int? portfolioId);
        PositionDTO CalculateAllParams(PositionDTO position);
        void UpdateAllPositionAndPortfolio();
        void UpdatePosition(int? id);
    }
}
