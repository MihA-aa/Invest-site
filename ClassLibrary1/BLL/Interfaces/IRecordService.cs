using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.DTO.Enums;

namespace BLL.Interfaces
{
    public interface IRecordService
    {
        IEnumerable<RecordDTO> GeRecords();
        IEnumerable<RecordDTO> GetRecordsByUserId(string userId);
        void CreateRecord(RecordDTO recordDTO);
        void CreateRecord(EntitiesDTO entity, OperationsDTO operation, string userId, int entityId, bool success);
    }
}
