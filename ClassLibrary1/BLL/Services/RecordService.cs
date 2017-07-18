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

        public void CreateRecord(string queryPath, string userId, bool success)
        {
            OperationsDTO operation;
            EntitiesDTO entity;
            int entityId = 0;
            try
            {
                var parameters = queryPath.Split('/');
                if (parameters[2] == "Save")
                {
                    if (parameters.Length == 4 && parameters[3] != "0")
                        parameters[2] = "Update";
                    else
                        parameters[2] = "Create";
                }
                operation = parameters[2].ToEnum<OperationsDTO>();
                entity = parameters[1].ToEnum<EntitiesDTO>();
                if(parameters.Length == 4)
                    entityId = Convert.ToInt32(parameters[3]);
            }
            catch (Exception ex)
            {
                //throw new ValidationException("Incorrect Path" + ex.Message, "");
                return;
            }

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
