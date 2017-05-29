using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IPositionService
    {
        PositionDTO GetPosition(int? id);
        IEnumerable<PositionDTO> GetPositions();
        void CreatePosition(PositionDTO position, int? portfolioId);
        void DeletePosition(int? id);
        void UpdatePosition(PositionDTO position);
        void CreateOrUpdatePosition(PositionDTO position, int? portfolioId);
        void AddPositionToPortfolio(Position position, int? portfolioId);
        PositionDTO CalculateAllParams(PositionDTO position);
    }
}
