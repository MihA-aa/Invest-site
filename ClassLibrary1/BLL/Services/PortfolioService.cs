using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Infrastructure;
using AutoMapper;
using BLL.Helpers;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class PortfolioService : BaseService, IPortfolioService
    {
        ICustomerService customerService { get; }
        IPositionService positionService { get; }

        public PortfolioService(IUnitOfWork uow, IValidateService vd, ICustomerService cs, IMapper map, IPositionService ps) : base(uow, vd, map)
        {
            customerService = cs;
            positionService = ps;
        }
        
        public IEnumerable<PortfolioDTO> GetPortfolios()
        {
            return IMapper.Map<IEnumerable<Portfolio>, List<PortfolioDTO>>(db.Portfolios.GetAll());
        }
        public IEnumerable<PortfolioDTO> GetPortfoliosForUser(string id)
        {
            var profile = db.Profiles.Get(id);
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
                UpdatePortfolioNameAndNotes(portfolioDto);
                return portfolioDto.Id;
            }
            else
            {
                var portfolioId = CreatePortfolio(portfolioDto, userId);
                return portfolioId;
            }
        }
        public void DeletePortfolio(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.PortfolioIdNotSet, "");
            if (!db.Portfolios.IsExist(id.Value))
                throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
            db.Portfolios.Delete(id.Value);
            db.Save();
        }

        public int CreatePortfolio(PortfolioDTO portfolioDto, string userId)
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
            db.Save();
            return portfolio.Id;
        }

        public void UpdatePortfolioNameAndNotes(PortfolioDTO portfolioDto)
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
            db.Save();
        }

        public void UpdatePortfolio(PortfolioDTO portfolioDto)
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
            db.Save();
        }

        public void UpdatePortfolio(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.PortfolioIdNotSet, "");
            var portfolio = db.Portfolios.GetPortfolioQuery(id.Value).FirstOrDefault();
            if (portfolio == null)
                throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
            var transaction = db.BeginTransaction();
            try
            {
                foreach (var Id in portfolio.Positions.Select(p => p.Id))
                {
                    positionService.UpdateOnlyPosition(Id);
                }
                db.Commit(transaction);
            }
            catch (Exception)
            {
                db.RollBack(transaction);
            }
            RecalculatePortfolioValue(id);
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
