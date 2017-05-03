using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Infrastructure;
using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    class PortfolioService : IPortfolioService
    {
        IUnitOfWork db { get; set; }

        public PortfolioService(IUnitOfWork uow)
        {
            db = uow;
        }

        public IEnumerable<PositionDTO> GetPositions()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Position, PositionDTO>());
            return Mapper.Map<IEnumerable<Position>, List<PositionDTO>>(db.Positions.GetAll());
        }

        public IEnumerable<PositionDTO> GetPortfolioPositions(int? portfolioId)
        {
            if (portfolioId == null)
                throw new ValidationException("Not set id of position", "");
            var portfolio = db.Portfolios.Get(portfolioId.Value);
            if (portfolio == null)
                throw new ValidationException("Portfolio not found", "");

            Mapper.Initialize(cfg => cfg.CreateMap<Position, PositionDTO>());
            return Mapper.Map<IEnumerable<Position>, List<PositionDTO>>(portfolio.Positions.ToList());
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
    }
}
