using System.Collections.Generic;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Resources;

namespace BLL.Services
{
    public class PositionService : IPositionService
    {
        IUnitOfWork db { get; }
        IValidateService validateService { get; }

        public PositionService(IUnitOfWork uow, IValidateService vd)
        {
            db = uow;
            validateService = vd;
        }

        public PositionDTO GetPosition(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.PositionIdNotSet, "");
            var position = db.Positions.Get(id.Value);
            if (position == null)
                throw new ValidationException(Resource.PositionNotFound, "");
            Mapper.Initialize(cfg => cfg.CreateMap<Position, PositionDTO>());
            return Mapper.Map<Position, PositionDTO>(position);
        }

        public IEnumerable<PositionDTO> GetPositions()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Position, PositionDTO>());
            return Mapper.Map<IEnumerable<Position>, List<PositionDTO>>(db.Positions.GetAll());
        }

        public void CreatePosition(PositionDTO position)
        {
            validateService.Validate(position);
            Mapper.Initialize(cfg => cfg.CreateMap<PositionDTO, Position>());
            var newPosition =  Mapper.Map<PositionDTO, Position>(position);
            if (db.Positions.CheckIfPositionExists(position.Id))
            {
                db.Positions.Update(newPosition);
            }
            else
            {
                db.Positions.Create(newPosition);
            }
            db.Save();
        }
        public void DeletePosition(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.PositionIdNotSet, "");
            var position = db.Positions.Get(id.Value);
            if (position == null)
                throw new ValidationException(Resource.PositionNotFound, "");
            db.Positions.Delete(id.Value);
            db.Save();
        }
        public void UpdatePosition(PositionDTO position)
        {
            if (position == null)
                throw new ValidationException(Resource.PositionNullReference, "");
            validateService.Validate(position);
            var position1 = db.Positions.Get(position.Id);
            if (position1 == null)
                throw new ValidationException(Resource.PositionNotFound, "");
            Mapper.Initialize(cfg => cfg.CreateMap<PositionDTO, Position>());
            Position newPosition = Mapper.Map<PositionDTO, Position>(position);
            db.Positions.Update(newPosition);
            db.Save();
        }
    }
}
