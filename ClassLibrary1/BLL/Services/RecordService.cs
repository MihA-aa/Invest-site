using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.DTO.Enums;
using BLL.Helpers;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class RecordService: BaseService, IRecordService
    {
        public RecordService(IUnitOfWork uow, IValidateService vd, IMapper map) : base(uow, vd, map){}
        public IEnumerable<RecordDTO> GeRecords()
        {
            return IMapper.Map<IEnumerable<Record>, List<RecordDTO>>(db.Records.GetAll());
        }

        public IEnumerable<RecordDTO> GetRecordsByUserId(string userId)
        {
            return IMapper.Map<IEnumerable<Record>, List<RecordDTO>>(db.Records.GetAll().Where(r=>r.UserId == userId));
        }

        public RecordDTO GetRecord(int? id)
        {
            if (id == null)
                throw new ValidationException("Record Id Not Set", "");
            var record = db.Records.Get(id.Value);
            if (record == null)
                throw new ValidationException("Record Not Found", "");
            return IMapper.Map<Record, RecordDTO>(record);
        }

        public int CreateRecord(EntitiesDTO entity, OperationsDTO operation, string userId, int entityId = 0, bool success = false)
        {
            var record = new RecordDTO
            {
                UserId = userId,
                Entity = entity,
                Operation = operation,
                Successfully = success,
                EntityId = entityId,
                DateTime = DateTime.Now
            };
            int recordId = CreateRecord(record);
            return recordId;
        }

        public int CreateRecord(RecordDTO recordDTO)
        {
            if (recordDTO == null)
                throw new ValidationException("Record Null Reference", "");
            var record = IMapper.Map<RecordDTO, Record>(recordDTO);
            db.Records.Create(record);
            db.Save();
            return record.Id;
        }

        public void DeleteRecord(int? id)
        {
            if (id == null)
                throw new ValidationException("Record Id Not Set", "");
            if (!db.Records.IsExist(id.Value))
                throw new ValidationException("Record Not Found", "");
            db.Records.Delete(id.Value);
            db.Save();
        }

        public void EstablishSuccess(int? id)
        {
            if (id == null)
                throw new ValidationException("Record Id Not Set", "");
            var record = db.Records.Get(id.Value);
            if (record == null)
                throw new ValidationException("Record Not Found", "");
            record.Successfully = true;
            db.Records.Update(record);
            db.Save();
        }

        public void SetEntityId(int? entityId, int? recordId)
        {
            if (entityId == null)
                throw new ValidationException("Entity Id Not Set", "");
            if (recordId == null)
                throw new ValidationException("Record Id Not Set", "");
            var record = db.Records.Get(recordId.Value);
            if (record == null)
                throw new ValidationException("Record Not Found", "");
            record.EntityId = entityId.Value;
            db.Records.Update(record);
            db.Save();
        }
    }
}
