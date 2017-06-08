using System;
using System.Collections.Generic;
using System.Linq;
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
        IMapper IMapper { get; }

        public PositionService(IUnitOfWork uow, IValidateService vd, ITradeSybolService tss, 
                                                        ICalculationService cs, IMapper map)
        {
            db = uow;
            validateService = vd;
            tradeSybolService = tss;
            calculationService = cs;
            IMapper = map;
        }

        public PositionDTO GetPosition(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.PositionIdNotSet, "");
            var position = db.Positions.Get(id.Value);
            if (position == null)
                throw new ValidationException(Resource.Resource.PositionNotFound, "");
            return IMapper.Map<Position, PositionDTO>(position);
        }

        public IEnumerable<PositionDTO> GetPositions()
        {
            return IMapper.Map<IEnumerable<Position>, List<PositionDTO>>(db.Positions.GetAll());
        }

        public void CreateOrUpdatePosition(PositionDTO position, int? portfolioId)
        {
            if (position == null)
                throw new ValidationException(Resource.Resource.PositionNullReference, "");
            if (db.Positions.IsExist(position.Id))
                UpdatePosition(position);
            else
                CreatePosition(position, portfolioId);
        }

        public PositionDTO CalculateAllParams(PositionDTO position)
        {
            if (position.CloseDate == new DateTime(1, 1, 1, 0, 0, 0))
            {
                position.CloseDate = null;
                position.CurrentPrice = tradeSybolService.GetPriceForDate(DateTime.Now.Date, position.SymbolId);
            }
            else
            {
                position.CurrentPrice = null;
            }
            var dividends = db.SymbolDividends.GetDividendsInDateInterval(position.OpenDate, position.CloseDate ?? DateTime.Now, position.SymbolId);  //39817
            position.Dividends = calculationService.GetDividends(dividends, position.OpenWeight);
            position.AbsoluteGain = calculationService.GetAbsoluteGain(position.CurrentPrice, position.ClosePrice,
                position.OpenPrice, position.OpenWeight, position.Dividends, position.TradeType);
            position.Gain = calculationService.GetGain(position.CurrentPrice, position.ClosePrice,
            position.OpenPrice, position.OpenWeight, position.Dividends, position.TradeType);
            var tradeInfo = tradeSybolService.GetMaxGainForSymbolBetweenDate(position.OpenDate, position.CloseDate ?? DateTime.Now,
                position.SymbolId, position.TradeType);
            if (tradeInfo != null)
            {
                position.MaxGain = calculationService.GetGain(tradeInfo.Price, position.ClosePrice,
                    position.OpenPrice, position.OpenWeight, tradeInfo.Dividends, position.TradeType);
            }
            TradeSybolViewDTO info = tradeSybolService.GetPriceAndDateLastUpdate(position.SymbolId);
            if (info == null)
            {
                position.LastUpdateDate = null;
                position.LastUpdatePrice = null;
            }
            else
            {
                position.LastUpdateDate = info.TradeDate;
                position.LastUpdatePrice = info.TradeIndex;
            }
            return position;
        }

        public void CreatePosition(PositionDTO positionDto, int? portfolioId)
        {
            if (positionDto == null)
                throw new ValidationException(Resource.Resource.PositionNullReference, "");
            if (portfolioId == null)
                throw new ValidationException(Resource.Resource.PortfolioIdNotSet, "");
            validateService.Validate(positionDto);
            positionDto = CalculateAllParams(positionDto);
            var position = IMapper.Map<PositionDTO, Position>(positionDto);
            db.Positions.Create(position);
            AddPositionToPortfolio(position, portfolioId);
            db.Save();
            db.Portfolios.RecalculatePortfolioValue(portfolioId.Value);
        }

        public void AddPositionToPortfolio(Position position, int? portfolioId)
        {
            if (position == null)
                throw new ValidationException(Resource.Resource.PositionNullReference, "");
            if (portfolioId == null)
                throw new ValidationException(Resource.Resource.PortfolioIdNotSet, "");
            if (!db.Portfolios.IsExist(portfolioId.Value))
                throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
            db.Portfolios.AddPositionToPortfolio(position, portfolioId.Value);
        }

        public void DeletePosition(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.PositionIdNotSet, "");
            if (!db.Positions.IsExist(id.Value))
                throw new ValidationException(Resource.Resource.PositionNotFound, "");
            db.Positions.Delete(id.Value);
            db.Save();
        }

        public void UpdatePosition(PositionDTO positionDto)
        {
            if (positionDto == null)
                throw new ValidationException(Resource.Resource.PositionNullReference, "");
            if (!db.Positions.IsExist(positionDto.Id))
                throw new ValidationException(Resource.Resource.PositionNotFound, "");
            validateService.Validate(positionDto);
            positionDto = CalculateAllParams(positionDto);
            var position = IMapper.Map<PositionDTO, Position>(positionDto);
            db.Positions.Update(position);
            db.Save();
            Portfolio portfolio = db.Portfolios.GetAll()
                .FirstOrDefault(x => x.Positions.Any(p => p.Id == positionDto.Id));
            db.Portfolios.RecalculatePortfolioValue(portfolio.Id);
        }
    }
}
