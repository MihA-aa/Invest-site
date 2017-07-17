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
        
        public void CreateRecord(EntitiesDTO entity, OperationsDTO operation, string userId, int entityId, bool success)
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
            CreateRecord(record);
        }

        public void CreateRecord(RecordDTO recordDTO)
        {
            if (db.SessionIsOpen())
            {
                if (recordDTO == null)
                    throw new ValidationException("Record Null Reference", "");
                var record = IMapper.Map<RecordDTO, Record>(recordDTO);
                db.Records.Create(record);
            }
            else
            {
                db.BeginTransaction();
                try
                {
                    if (recordDTO == null)
                        throw new ValidationException("Record Null Reference", "");
                    var record = IMapper.Map<RecordDTO, Record>(recordDTO);
                    db.Records.Create(record);
                    
                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.RollBack();
                    throw ex;
                }
            }
            
        }
    }
}
