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
    public class PositionService :BaseService, IPositionService
    {
        ITradeSybolService tradeSybolService { get; }
        ICalculationService calculationService { get; }

        public PositionService(IUnitOfWork uow, IValidateService vd, ITradeSybolService tss, 
                               ICalculationService cs, IMapper map) : base(uow, vd, map)
        {
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
            return IMapper.Map<Position, PositionDTO>(position);
        }

        public IEnumerable<PositionDTO> GetPositions()
        {
            return IMapper.Map<IEnumerable<Position>, List<PositionDTO>>(db.Positions.GetAll());
        }

        public IEnumerable<PositionDTO> GetPositionsForUser(string id)
        {
            var profile = db.Profiles.Get(id);
            var positions = profile?.Customer?.Portfolios?.SelectMany(p => p.Positions);
            return IMapper.Map<IEnumerable<Position>, List<PositionDTO>>(positions);
        }

        public bool CheckAccess(string userId, int? positionId)
        {
            if (positionId == null)
                throw new ValidationException(Resource.Resource.PositionIdNotSet, "");
            if (userId == null)
                throw new ValidationException(Resource.Resource.ProfileIdNotSet, "");
            var profile = db.Profiles.Get(userId);
            var positions = profile?.Customer?.Portfolios?.SelectMany(p => p.Positions);
            bool access = positions?.FirstOrDefault(p => p.Id == positionId) != null;
            return access;
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
        
        public Dictionary<double, decimal> GetChartForPosition(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.PositionIdNotSet, "");
            var position = db.Positions.Get(id.Value);
            if (position == null)
                throw new ValidationException(Resource.Resource.PositionNotFound, "");

            var tradeInfo = tradeSybolService.GetDateForSymbolInDateInterval(position.OpenDate, position.CloseDate ?? DateTime.Now,
                position.SymbolId);
            var dates = tradeInfo.Select(d => HelperService.ConvertToUnixTimestamp(d.TradeDate)*1000);
            var gains = tradeInfo.Select(d => calculationService.GetGain(d.Price, position.ClosePrice,
                             position.OpenPrice, position.OpenWeight, d.Dividends, (TradeTypesDTO)position.TradeType));
            var dic = dates.Zip(gains, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
            return dic;
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
        }

        public void UpdatePosition(PositionDTO positionDto)
        {
            if (positionDto == null)
                throw new ValidationException(Resource.Resource.PositionNullReference, "");
            if (!db.Positions.IsExist(positionDto.Id))
                throw new ValidationException(Resource.Resource.PositionNotFound, "");
            validateService.Validate(positionDto);

            positionDto = CalculateAllParams(positionDto);
            var positionFromDb = db.Positions.Get(positionDto.Id);
            var position = IMapper.Map<PositionDTO, Position>(positionDto);
            position.Portfolio = positionFromDb.Portfolio;

            db.Positions.Update(position);

            Portfolio portfolio = db.Portfolios.GetAll()
                .FirstOrDefault(x => x.Positions.Any(p => p.Id == positionDto.Id));
            if (portfolio != null)
                db.Portfolios.RecalculatePortfolioValue(portfolio.Id);
        }

        public void UpdateOnlyPosition(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.PositionIdNotSet, "");
            var position = db.Positions.GetPositionQuery(id.Value).FirstOrDefault();
            if (position == null)
                throw new ValidationException(Resource.Resource.PositionNotFound, "");
            var positionDto = CalculateAllParams(IMapper.Map<Position, PositionDTO>(position));
            var newposition = IMapper.Map<PositionDTO, Position>(positionDto);
            db.Positions.Update(newposition);
        }

        public void UpdatePosition(int? id)
        {
            UpdateOnlyPosition(id);
            var portfolio = db.Portfolios.GetAll()
                .FirstOrDefault(x => x.Positions.Any(p => p.Id == id));
            db.Portfolios.RecalculatePortfolioValue(portfolio.Id);
        }

        public void UpdateAllPositionAndPortfolio()
        {
                var positions = IMapper.Map<IQueryable<Position>, List<PositionDTO>>(db.Positions.GetPositionsQuery());
                for (int i = 0; i < positions.Count(); i++)
                {
                    positions[i] = CalculateAllParams(positions[i]);
                    var position = IMapper.Map<PositionDTO, Position>(positions[i]);
                    db.Positions.Update(position);
                }
                var portfoliosId = db.Portfolios.GetAll().Select(p => p.Id);
                foreach (var id in portfoliosId)
                {
                    db.Portfolios.RecalculatePortfolioValue(id);
                }
        }

        public PositionDTO CalculateAllParams(PositionDTO position)
        {
            if (position.CloseDate == new DateTime(1, 1, 1, 0, 0, 0) || position.CloseDate == null)
            {
                position.CloseDate = null;
                position.CurrentPrice = tradeSybolService.GetPriceForDate(DateTime.Now.Date, position.SymbolId);
            }
            else
            {
                position.ClosePrice = tradeSybolService.GetPriceForDate(position.CloseDate.Value, position.SymbolId);
                position.CurrentPrice = null;
            }
            var dividends = db.SymbolDividends.GetDividendsInDateInterval(position.OpenDate, position.CloseDate ?? DateTime.Now, position.SymbolId);
            position.Dividends = calculationService.GetDividends(dividends, position.OpenWeight);
            position.AbsoluteGain = calculationService.GetAbsoluteGain(position.CurrentPrice, position.ClosePrice,
                position.OpenPrice, position.OpenWeight, position.Dividends, position.TradeType);
            position.Gain = calculationService.GetGain(position.CurrentPrice, position.ClosePrice,
                position.OpenPrice, position.OpenWeight, position.Dividends, position.TradeType);
            var tradeInfo = tradeSybolService.GetMaxGainForSymbolBetweenDate(position.OpenDate, position.CloseDate ?? DateTime.Now,
                                                                             position.SymbolId, position.TradeType);
            if (tradeInfo != null)
            {
                position.MaxGain = calculationService.GetGain(tradeInfo.Price, position.ClosePrice, position.OpenPrice,
                                                              position.OpenWeight, tradeInfo.Dividends, position.TradeType);
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
    }
}
