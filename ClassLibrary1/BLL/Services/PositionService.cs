using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.DTO;
using BLL.DTO.Enums;
using BLL.Helpers;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class PositionService : IPositionService
    {
        IUnitOfWork db { get; }
        IValidateService validateService { get; }
        ITradeSybolService tradeSybolService { get; }
        ICalculationService calculationService { get; }

        public PositionService(IUnitOfWork uow, IValidateService vd, ITradeSybolService tss, ICalculationService cs)
        {
            db = uow;
            validateService = vd;
            tradeSybolService = tss;
            calculationService = cs;
        }

        public PositionDTO GetPosition(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.PositionIdNotSet, "");
            var position = db.Positions.Get(id.Value);
            if (position == null)
                throw new ValidationException(Resource.Resource.PositionNotFound, "");
            return MapperHelper.ConvertPositionToPositionDto(position);
        }

        public IEnumerable<PositionDTO> GetPositions()
        {
            return MapperHelper.ConvertListPositionToPositionDto(db.Positions.GetAll());
        }

        public void CreateOrUpdatePosition(PositionDTO position, int? portfolioId)
        {
            if (position == null)
                throw new ValidationException(Resource.Resource.PositionNullReference, "");
            validateService.Validate(position);
            if (db.Positions.CheckIfPositionExists(position.Id))
            {
                UpdatePosition(position);
            }
            else
            {
                if (position.CloseDate == new DateTime(1, 1, 1, 0, 0, 0))
                    position.CloseDate = null;
                //if (position.TradeStatus == TradeStatusesDTO.Open)
                var dividends = db.SymbolDividends.Get(39817);  //position.SymbolId
                position.CurrentPrice = tradeSybolService.GetPriceForDate(DateTime.Now.Date, position.SymbolId);
                position.Dividends = calculationService.GetDividends(dividends.Dividends, position.OpenWeight);
                position.AbsoluteGain = calculationService.GetAbsoluteGain(position.CurrentPrice, position.ClosePrice,
                    position.OpenPrice, position.OpenWeight, position.Dividends, position.TradeType);
                position.Gain = calculationService.GetGain(position.CurrentPrice, position.ClosePrice,
                    position.OpenPrice, position.OpenWeight, position.Dividends, position.TradeType);

                var newPosition = MapperHelper.ConvertPositionDtoToPosition(position);
                CreatePosition(newPosition);
                AddPositionToPortfolio(newPosition, portfolioId);
            }
            db.Save();
        }


        public void CreatePosition(Position position)
        {
            db.Positions.Create(position);
            db.Save();
        }

        public void AddPositionToPortfolio(Position position, int? portfolioId)
        {
            if (portfolioId == null)
                throw new ValidationException(Resource.Resource.PortfolioIdNotSet, "");
            var portfolio1 = db.Portfolios.Get(portfolioId.Value);
            if (portfolio1 == null)
                throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
            db.Portfolios.AddPositionToPortfolio(position, portfolioId.Value);
        }

        public void DeletePosition(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.PositionIdNotSet, "");
            var position = db.Positions.Get(id.Value);
            if (position == null)
                throw new ValidationException(Resource.Resource.PositionNotFound, "");
            db.Positions.Delete(id.Value);
            db.Save();
        }
        public void UpdatePosition(PositionDTO position)
        {
            var newPosition = MapperHelper.ConvertPositionDtoToPosition(position);
            db.Positions.Update(newPosition);
            db.Save();
        }
    }
}
