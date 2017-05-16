﻿using BLL.Interfaces;
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
using Resources;

namespace BLL.Services
{
    public class PortfolioService : IPortfolioService
    {
        IUnitOfWork db { get; }
        IValidateService validateService { get; }

        public PortfolioService(IUnitOfWork uow, IValidateService vd)
        {
            db = uow;
            validateService = vd;
        }
        
        public IEnumerable<PortfolioDTO> GetPortfolios()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Portfolio, PortfolioDTO>());
            return Mapper.Map<IEnumerable<Portfolio>, List<PortfolioDTO>>(db.Portfolios.GetAll());
        }

        public IEnumerable<PositionDTO> GetPortfolioPositions(int? portfolioId)
        {
            if (portfolioId == null)
                throw new ValidationException(Resource.PortfolioIdNotSet, "");
            var portfolio = db.Portfolios.Get(portfolioId.Value);
            if (portfolio == null)
                throw new ValidationException(Resource.PortfolioNotFound, "");
            Mapper.Initialize(cfg => cfg.CreateMap<Position, PositionDTO>());
            return Mapper.Map<IEnumerable<Position>, List<PositionDTO>>(portfolio.Positions.ToList());
        }

        public PortfolioDTO GetPortfolio(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.PortfolioIdNotSet, "");
            var portfolio = db.Portfolios.Get(id.Value);
            if (portfolio == null)
                throw new ValidationException(Resource.PortfolioNotFound, "");
            Mapper.Initialize(cfg => cfg.CreateMap<Portfolio, PortfolioDTO>());
            return Mapper.Map<Portfolio, PortfolioDTO>(portfolio);
        }

        public int CreatePortfolio(PortfolioDTO portfolio)
        {
            validateService.Validate(portfolio);
            Mapper.Initialize(cfg => cfg.CreateMap<PortfolioDTO, Portfolio>()
                    .ForMember("LastUpdateDate", opt => opt.MapFrom(src => DateTime.Now)));
            Portfolio newPortfolio = Mapper.Map<PortfolioDTO, Portfolio>(portfolio);
            db.Portfolios.Create(newPortfolio);
            db.Save();
            return(newPortfolio.Id);
        }
        public void DeletePortfolio(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.PortfolioIdNotSet, "");
            var portfolio = db.Portfolios.Get(id.Value);
            if (portfolio == null)
                throw new ValidationException(Resource.PortfolioNotFound, "");
            db.Portfolios.Delete(id.Value);
        }

        public void UpdatePortfolio(PortfolioDTO portfolio)
        {
            if (portfolio == null)
                throw new ValidationException(Resource.PortfolioNullReference, "");
            validateService.Validate(portfolio);
            var portfolio1 = db.Portfolios.Get(portfolio.Id);
            if (portfolio1 == null)
                throw new ValidationException(Resource.PortfolioNotFound, "");
            Mapper.Initialize(cfg => cfg.CreateMap<PortfolioDTO, Portfolio>());
            Portfolio newPortfolio = Mapper.Map<PortfolioDTO, Portfolio>(portfolio);
            db.Portfolios.Update(newPortfolio);
        }

        public void UpdatePortfoliosDisplayIndex(Dictionary<string, string> portfolios)
        {
            foreach (var portfolio in portfolios)
            {
                db.Portfolios.ChangePortfolioDisplayIndex(Convert.ToInt32(portfolio.Key), Convert.ToInt32(portfolio.Value));
            }

        }

        public void AddPositionToPortfolio(PositionDTO position, int? portfolioId)
        {
            if (position == null)
                throw new ValidationException(Resource.PositionNullReference, "");
            if (portfolioId == null)
                throw new ValidationException(Resource.PortfolioIdNotSet, "");
            var portfolio1 = db.Portfolios.Get(portfolioId.Value);
            if (portfolio1 == null)
                throw new ValidationException(Resource.PortfolioNotFound, "");
            Mapper.Initialize(cfg => cfg.CreateMap<PositionDTO, Position>());
            Position newPosition = Mapper.Map<PositionDTO, Position>(position);
            db.Portfolios.AddPositionToPortfolio(newPosition, portfolioId.Value);
        }
    }
}
