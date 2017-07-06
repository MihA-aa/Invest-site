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
        RecordDTO GetRecord(int? id);
        IEnumerable<RecordDTO> GetRecordsByUserId(string userId);
        int CreateRecord(RecordDTO recordDTO);
        void DeleteRecord(int? id);
        void EstablishSuccess(int? id);
        void SetEntityId(int? entityId, int? recordId);
        int CreateRecord(EntitiesDTO entity, OperationsDTO operation, string userId, int entityId = 0, bool success = false);
    }
}
