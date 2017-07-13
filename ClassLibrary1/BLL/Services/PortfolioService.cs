using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Infrastructure;
using AutoMapper;
using BLL.DTO.Enums;
using BLL.Helpers;
using DAL.Entities;
using DAL.Enums;
using DAL.Interfaces;
using NHibernate;

namespace BLL.Services
{
    public class PortfolioService : BaseService, IPortfolioService
    {
        ICustomerService customerService { get; }
        IPositionService positionService { get; }
        IRecordService recordService { get; }

        public PortfolioService(IUnitOfWork uow, IValidateService vd, ICustomerService cs, IMapper map, 
                                 IPositionService ps, IRecordService rs) : base(uow, vd, map)
        {
            customerService = cs;
            positionService = ps;
            recordService = rs;
        }
        
        public IEnumerable<PortfolioDTO> GetPortfolios()
        {
            return IMapper.Map<IEnumerable<Portfolio>, List<PortfolioDTO>>(db.Portfolios.GetAll());
        }
        public IEnumerable<PortfolioDTO> GetPortfoliosForUser(string id)
        {
            //var profile = session.Get<DAL.Entities.Profile>(id); // Session.Query<Profile>().FirstOrDefault(p => p.Id == id);// 
            var profile = db.Profiles.Get(id);
            if (profile == null)
                throw new ValidationException(Resource.Resource.ProfileNotFound, "");
            return IMapper.Map<IEnumerable<Portfolio>, List<PortfolioDTO>>(profile?.Customer?.Portfolios);
        }

        public bool CheckAccess(string userId, int? portfolioId)
        {
            if (portfolioId == null)
                throw new ValidationException(Resource.Resource.PortfolioIdNotSet, "");
            if (userId == null)
                throw new ValidationException(Resource.Resource.ProfileIdNotSet, "");
            if (!db.Portfolios.IsExist(portfolioId.Value))
                throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
            return db.Profiles.ProfileAccess(userId, portfolioId.Value);
        }

        public IEnumerable<PositionDTO> GetPortfolioPositions(int? portfolioId)
        {
            if (portfolioId == null)
                throw new ValidationException(Resource.Resource.PortfolioIdNotSet, "");
            var portfolio = db.Portfolios.Get(portfolioId.Value);
            if (portfolio == null)
                throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
            return IMapper.Map<IEnumerable<Position>, List<PositionDTO>>(portfolio.Positions.ToList());
        }

        public IEnumerable<PositionDTO> GetPortfolioPositionsForUser(int? portfolioId, string id)
        {
            if (portfolioId == null)
                throw new ValidationException(Resource.Resource.PortfolioIdNotSet, "");
            var portfolios = db.Profiles.Get(id)?.Customer?.Portfolios;
            var portfolio = portfolios?.FirstOrDefault(p => p.Id == portfolioId.Value);
            if (portfolio == null)
                throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
            return IMapper.Map<IEnumerable<Position>, List<PositionDTO>>(portfolio.Positions.ToList());
        }

        public PortfolioDTO GetPortfolio(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.PortfolioIdNotSet, "");
            var portfolio = db.Portfolios.Get(id.Value);
            if (portfolio == null)
                throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
            return IMapper.Map<Portfolio, PortfolioDTO>(portfolio);
        }

        public int CreateOrUpdatePortfolio(PortfolioDTO portfolioDto, string userId)
        {
            if (portfolioDto == null)
                throw new ValidationException(Resource.Resource.PortfolioNullReference, "");
            if (db.Portfolios.IsExist(portfolioDto.Id))
            {
                UpdatePortfolioNameAndNotes(portfolioDto, userId);
                return portfolioDto.Id;
            }
            else
            {
                var portfolioId = CreatePortfolio(portfolioDto, userId);
                return portfolioId;
            }
        }

        public int CreatePortfolio(PortfolioDTO portfolioDto, string userId)
        {
            //var transaction = db.BeginTransaction();
            int portfolioId = 0;
            try
            {
                if (portfolioDto == null)
                    throw new ValidationException(Resource.Resource.PortfolioNullReference, "");
                validateService.Validate(portfolioDto);
                Mapper.Initialize(cfg => cfg.CreateMap<PortfolioDTO, Portfolio>()
                        .ForMember("LastUpdateDate", opt => opt.MapFrom(src => DateTime.Now))
                        .ForMember("DisplayIndex", opt => opt.MapFrom(src => db.Portfolios.Count() + 1)));
                var portfolio = Mapper.Map<PortfolioDTO, Portfolio>(portfolioDto);
                var customer = customerService.GetCustomerByProfileId(userId);
                portfolio.Customer = customer;
                customer.Portfolios.Add(portfolio);
                db.Portfolios.Create(portfolio);
                //db.Save();
                portfolioId = portfolio.Id;

                recordService.CreateRecord(EntitiesDTO.Portfolio, OperationsDTO.Create, userId, portfolioId, true);
                //db.Commit(transaction);
            }
            catch (Exception ex)
            {
                //db.RollBack(transaction);
                recordService.CreateRecord(EntitiesDTO.Portfolio, OperationsDTO.Create, userId, 0, false);
                throw ex;
            }
            //finally
            //{
            //    transaction.Dispose();
            //}
            return portfolioId;
        }

        public void UpdatePortfolioNameAndNotes(PortfolioDTO portfolioDto, string userId)
        {
            //var transaction = db.BeginTransaction();
            try
            {
                if (portfolioDto == null)
                    throw new ValidationException(Resource.Resource.PortfolioNullReference, "");
                if (!db.Portfolios.IsExist(portfolioDto.Id))
                    throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
                validateService.Validate(portfolioDto);
                Mapper.Initialize(cfg => cfg.CreateMap<PortfolioDTO, Portfolio>()
                        .ForMember("LastUpdateDate", opt => opt.MapFrom(src => DateTime.Now)));
                var portfolio = Mapper.Map<PortfolioDTO, Portfolio>(portfolioDto);
                db.Portfolios.UpdatePortfolioNameAndNotes(portfolio);
                //db.Save();

                recordService.CreateRecord(EntitiesDTO.Portfolio, OperationsDTO.Update, userId, portfolioDto.Id, true);
                //db.Commit(transaction);
            }
            catch (Exception ex)
            {
                //db.RollBack(transaction);
                recordService.CreateRecord(EntitiesDTO.Portfolio, OperationsDTO.Update, userId, portfolioDto?.Id ?? 0, false);
                throw ex;
            }
            finally
            {
                //transaction.Dispose();
            }
        }

        public void UpdatePortfolio(PortfolioDTO portfolioDto, string userId)
        {
            //var transaction = db.BeginTransaction();
            try
            {
                if (portfolioDto == null)
                    throw new ValidationException(Resource.Resource.PortfolioNullReference, "");
                if (!db.Portfolios.IsExist(portfolioDto.Id))
                    throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
                validateService.Validate(portfolioDto);
                Mapper.Initialize(cfg => cfg.CreateMap<PortfolioDTO, Portfolio>()
                        .ForMember("LastUpdateDate", opt => opt.MapFrom(src => DateTime.Now)));
                var portfolio = Mapper.Map<PortfolioDTO, Portfolio>(portfolioDto);
                db.Portfolios.Update(portfolio);
                //db.Save();

                recordService.CreateRecord(EntitiesDTO.Portfolio, OperationsDTO.Update, userId, portfolioDto.Id, true);
                //db.Commit(transaction);
            }
            catch (Exception ex)
            {
                //db.RollBack(transaction);
                recordService.CreateRecord(EntitiesDTO.Portfolio, OperationsDTO.Update, userId, portfolioDto?.Id ?? 0, false);
                throw ex;
            }
            finally
            {
                //transaction.Dispose();
            }
        }

        public void UpdatePortfolio(int? id)
        {
            //var transaction = db.BeginTransaction();
            try
            {
                if (id == null)
                    throw new ValidationException(Resource.Resource.PortfolioIdNotSet, "");
                var portfolio = db.Portfolios.GetPortfolioQuery(id.Value).FirstOrDefault();
                if (portfolio == null)
                    throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
                foreach (var Id in portfolio.Positions.Select(p => p.Id))
                {
                    positionService.UpdateOnlyPosition(Id);
                }
                RecalculatePortfolioValue(id);

                //db.Commit(transaction);
            }
            catch (Exception ex)
            {
                //db.RollBack(transaction);
                throw ex;
            }
            finally
            {
                //transaction.Dispose();
            }
        }

        public void DeletePortfolio(int? id, string userId)
        {
            //var transaction = db.BeginTransaction();
            try
            {
                if (id == null)
                    throw new ValidationException(Resource.Resource.PortfolioIdNotSet, "");
                if (!db.Portfolios.IsExist(id.Value))
                    throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
                db.Portfolios.Delete(id.Value);
                //db.Save();

                recordService.CreateRecord(EntitiesDTO.Portfolio, OperationsDTO.Update, userId, id.Value, true);
                //db.Commit(transaction);
            }
            catch (Exception ex)
            {
                //db.RollBack(transaction);
                recordService.CreateRecord(EntitiesDTO.Portfolio, OperationsDTO.Update, userId, id ?? 0, false);
                throw ex;
            }
            finally
            {
                //transaction.Dispose();
            }
        }

        public void UpdatePortfoliosDisplayIndex(Dictionary<string, string> portfolios)
        {
            foreach (var portfolio in portfolios)
            {
                db.Portfolios.ChangePortfolioDisplayIndex(Convert.ToInt32(portfolio.Key), Convert.ToInt32(portfolio.Value));
            }
        }

        public void RecalculatePortfolioValue(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.PortfolioIdNotSet, "");
            var portfolio = db.Portfolios.Get(id.Value);
            if (portfolio == null)
                throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
            db.Portfolios.RecalculatePortfolioValue(id.Value);
        }
    }
}
