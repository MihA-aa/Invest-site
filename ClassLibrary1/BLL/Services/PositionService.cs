using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class PositionService : IPositionService
    {
        IUnitOfWork db { get; set; }
        private ValidateService validateService = new ValidateService();

        public PositionService(IUnitOfWork uow)
        {
            db = uow;
        }

        public PositionDTO GetPosition(int? id)
        {
            if (id == null)
                throw new ValidationException("Not set id of position", "");
            var position = db.Positions.Get(id.Value);
            if (position == null)
                throw new ValidationException("Position not found", "");
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
            //Validation!!!!!!!!!
            validateService.IsValid(position);
            Mapper.Initialize(cfg => cfg.CreateMap<PositionDTO, Position>());
            Position newPosition =  Mapper.Map<PositionDTO, Position>(position);
            db.Positions.Create(newPosition);
        }
        public void DeletePosition(int? id)
        {
            if (id == null)
                throw new ValidationException("Not set id of position", "");
            var position = db.Positions.Get(id.Value);
            if (position == null)
                throw new ValidationException("Position not found", "");
            db.Positions.Delete(id.Value);
        }
        public void UpdatePosition(PositionDTO position)
        {
            if (position == null)
                throw new ValidationException("Position is null reference", "");
            var position1 = db.Positions.Get(position.Id);
            if (position1 == null)
                throw new ValidationException("Position not found", "");
            Mapper.Initialize(cfg => cfg.CreateMap<PositionDTO, Position>());
            Position newPosition = Mapper.Map<PositionDTO, Position>(position);
            db.Positions.Update(newPosition);
        }
    }
}
