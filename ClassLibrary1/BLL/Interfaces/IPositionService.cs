using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IPositionService
    {
        PositionDTO GetPosition(int? id);
        IEnumerable<PositionDTO> GetPositions();
        void CreatePosition(PositionDTO position);
        void DeletePosition(int? id);
        void UpdatePosition(PositionDTO position);
    }
}
