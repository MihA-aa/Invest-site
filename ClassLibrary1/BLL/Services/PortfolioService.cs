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
    public class PortfolioService : IPortfolioService
    {
        IUnitOfWork db { get; }
        IValidateService validateService { get; }
        ICustomerService customerService { get; }
        IMapper IMapper { get; }

        public PortfolioService(IUnitOfWork uow, IValidateService vd, ICustomerService cs, IMapper map)
        {
            db = uow;
            validateService = vd;
            customerService = cs;
            IMapper = map;
        }
        
        public IEnumerable<PortfolioDTO> GetPortfolios()
        {
            return IMapper.Map<IEnumerable<Portfolio>, List<PortfolioDTO>>(db.Portfolios.GetAll());
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
            validateService.Validate(portfolioDto);
            Mapper.Initialize(cfg => cfg.CreateMap<PortfolioDTO, Portfolio>()
                    .ForMember("LastUpdateDate", opt => opt.MapFrom(src => DateTime.Now))
                    .ForMember("DisplayIndex", opt => opt.MapFrom(src => db.Portfolios.Count() + 1)));
            var portfolio = Mapper.Map<PortfolioDTO, Portfolio>(portfolioDto);
            if (db.Portfolios.IsExist(portfolioDto.Id))
            {
                db.Portfolios.UpdatePortfolioNameAndNotes(portfolio);               
            }
            else
            {
                portfolio.Customer = customerService.GetCustomerByProfileId(userId);
                db.Portfolios.Create(portfolio);
            }
            db.Save();
            return (portfolio.Id);
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

        public void CreatePortfolio(PortfolioDTO portfolioDto, string userId)
        {
            if (portfolioDto == null)
                throw new ValidationException(Resource.Resource.PortfolioNullReference, "");
            validateService.Validate(portfolioDto);
            Mapper.Initialize(cfg => cfg.CreateMap<PortfolioDTO, Portfolio>()
                    .ForMember("LastUpdateDate", opt => opt.MapFrom(src => DateTime.Now))
                    .ForMember("DisplayIndex", opt => opt.MapFrom(src => db.Portfolios.Count() + 1)));
            var portfolio = Mapper.Map<PortfolioDTO, Portfolio>(portfolioDto);
            portfolio.Customer = customerService.GetCustomerByProfileId(userId);
            db.Portfolios.Create(portfolio);
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

        public void UpdatePortfoliosDisplayIndex(Dictionary<string, string> portfolios)
        {
            foreach (var portfolio in portfolios)
            {
                db.Portfolios.ChangePortfolioDisplayIndex(Convert.ToInt32(portfolio.Key), Convert.ToInt32(portfolio.Value));
            }
        }

        public void RecalculatePortfolioValue(int id)
        {
            var portfolio = db.Portfolios.Get(id);
            if (portfolio == null)
                throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
            db.Portfolios.RecalculatePortfolioValue(id);
        }
    }
}
