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
using Position = DAL.Entities.Position;

namespace BLL.Services
{
    public class PositionService :BaseService, IPositionService
    {
        ITradeSybolService tradeSybolService { get; }
        ICalculationService calculationService { get; }
        IRecordService recordService { get; }

        public PositionService(IUnitOfWork uow, IValidateService vd, ITradeSybolService tss, 
                               ICalculationService cs, IMapper map, IRecordService rs) : base(uow, vd, map)
        {
            tradeSybolService = tss;
            calculationService = cs;
            recordService = rs;
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
            if (positions?.FirstOrDefault(p => p.Id == positionId) != null)
                return true;
            return false;
        }

        public void CreateOrUpdatePosition(PositionDTO position, int? portfolioId, string userId)
        {
            if (position == null)
                throw new ValidationException(Resource.Resource.PositionNullReference, "");
            if (db.Positions.IsExist(position.Id))
                UpdatePosition(position, userId);
            else
                CreatePosition(position, portfolioId, userId);
        }
        
        public Dictionary<double, decimal> GetChartForPosition(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.PositionIdNotSet, "");
            var position = IMapper.Map<Position, PositionDTO>(db.Positions.Get(id.Value));
            if (position == null)
                throw new ValidationException(Resource.Resource.PositionNotFound, "");
            var tradeInfo = tradeSybolService.GetDateForSymbolInDateInterval(position.OpenDate, position.CloseDate ?? DateTime.Now,
                position.SymbolId);
            var dates = tradeInfo.Select(d => HelperService.ConvertToUnixTimestamp(d.TradeDate)*1000);
            var gains = tradeInfo.Select(d => calculationService.GetGain(d.Price, position.ClosePrice,
                             position.OpenPrice, position.OpenWeight, d.Dividends, position.TradeType));
            var dic = dates.Zip(gains, (k, v) => new { k, v })
              .ToDictionary(x => x.k, x => x.v);
            return dic;
        }

        public void CreatePosition(PositionDTO positionDto, int? portfolioId, string userId)
        {
            var transaction = db.BeginTransaction();
            try
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

                recordService.CreateRecord(EntitiesDTO.Position, OperationsDTO.Create, userId, position.Id, true);
                db.Commit(transaction);
            }
            catch (Exception ex)
            {
                db.RollBack(transaction);
                recordService.CreateRecord(EntitiesDTO.Position, OperationsDTO.Create, userId, 0, false);
                throw ex;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public void AddPositionToPortfolio(Position position, int? portfolioId)
        {
            var transaction = db.BeginTransaction();
            try
            {
                if (position == null)
                    throw new ValidationException(Resource.Resource.PositionNullReference, "");
                if (portfolioId == null)
                    throw new ValidationException(Resource.Resource.PortfolioIdNotSet, "");
                if (!db.Portfolios.IsExist(portfolioId.Value))
                    throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
                db.Portfolios.AddPositionToPortfolio(position, portfolioId.Value);

                db.Commit(transaction);
            }
            catch (Exception ex)
            {
                db.RollBack(transaction);
                throw ex;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public void DeletePosition(int? id, string userId)
        {
            var transaction = db.BeginTransaction();
            try
            {
                if (id == null)
                    throw new ValidationException(Resource.Resource.PositionIdNotSet, "");
                if (!db.Positions.IsExist(id.Value))
                    throw new ValidationException(Resource.Resource.PositionNotFound, "");
                db.Positions.Delete(id.Value);
                db.Save();

                recordService.CreateRecord(EntitiesDTO.Position, OperationsDTO.Delete, userId, id.Value, true);
                db.Commit(transaction);
            }
            catch (Exception ex)
            {
                db.RollBack(transaction);
                recordService.CreateRecord(EntitiesDTO.Position, OperationsDTO.Delete, userId, id ?? 0, false);
                throw ex;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public void UpdatePosition(PositionDTO positionDto, string userId)
        {
            var transaction = db.BeginTransaction();
            try
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
                if (portfolio != null)
                    db.Portfolios.RecalculatePortfolioValue(portfolio.Id);

                recordService.CreateRecord(EntitiesDTO.Position, OperationsDTO.Update, userId, positionDto.Id, true);
                db.Commit(transaction);
            }
            catch (Exception ex)
            {
                db.RollBack(transaction);
                recordService.CreateRecord(EntitiesDTO.Position, OperationsDTO.Update, userId, positionDto?.Id ?? 0, false);
                throw ex;
            }
            finally
            {
                transaction.Dispose();
            }
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
            db.Save();
        }

        public void UpdatePosition(int? id)
        {
            var transaction = db.BeginTransaction();
            try
            {
                UpdateOnlyPosition(id);
                Portfolio portfolio = db.Portfolios.GetAll()
                    .FirstOrDefault(x => x.Positions.Any(p => p.Id == id));
                db.Portfolios.RecalculatePortfolioValue(portfolio.Id);

                db.Commit(transaction);
            }
            catch (Exception ex)
            {
                db.RollBack(transaction);
                throw ex;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public void UpdateAllPositionAndPortfolio()
        {
            var transaction = db.BeginTransaction();
            try
            {
                var positions = IMapper.Map<IQueryable<Position>, List<PositionDTO>>(db.Positions.GetPositionsQuery());
                for (int i = 0; i < positions.Count(); i++)
                {
                    positions[i] = CalculateAllParams(positions[i]);
                    var position = IMapper.Map<PositionDTO, Position>(positions[i]);
                    db.Positions.Update(position);
                }
                db.Save();
                var portfoliosId = db.Portfolios.GetAll().Select(p => p.Id);
                foreach (var id in portfoliosId)
                {
                    db.Portfolios.RecalculatePortfolioValue(id);
                }
            }
            catch (Exception ex)
            {
                db.RollBack(transaction);
                throw ex;
            }
            finally
            {
                transaction.Dispose();
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
