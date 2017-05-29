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

        public PortfolioService(IUnitOfWork uow, IValidateService vd, ICustomerService cs)
        {
            db = uow;
            validateService = vd;
            customerService = cs;
        }
        
        public IEnumerable<PortfolioDTO> GetPortfolios()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Portfolio, PortfolioDTO>());
            return Mapper.Map<IEnumerable<Portfolio>, List<PortfolioDTO>>(db.Portfolios.GetAll());
        }

        public IEnumerable<PositionDTO> GetPortfolioPositions(int? portfolioId)
        {
            if (portfolioId == null)
                throw new ValidationException(Resource.Resource.PortfolioIdNotSet, "");
            var portfolio = db.Portfolios.Get(portfolioId.Value);
            if (portfolio == null)
                throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
            return MapperHelper.ConvertListPositionToPositionDto(portfolio.Positions.ToList());
        }

        public PortfolioDTO GetPortfolio(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.PortfolioIdNotSet, "");
            var portfolio = db.Portfolios.Get(id.Value);
            if (portfolio == null)
                throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
            Mapper.Initialize(cfg => cfg.CreateMap<Portfolio, PortfolioDTO>());
            return Mapper.Map<Portfolio, PortfolioDTO>(portfolio);
        }

        public int CreateOrUpdatePortfolio(PortfolioDTO portfolio, string userId)
        {
            if (portfolio == null)
                throw new ValidationException(Resource.Resource.PortfolioNullReference, "");           
            validateService.Validate(portfolio);
            Mapper.Initialize(cfg => cfg.CreateMap<PortfolioDTO, Portfolio>()
                    .ForMember("LastUpdateDate", opt => opt.MapFrom(src => DateTime.Now))
                    .ForMember("DisplayIndex", opt => opt.MapFrom(src => db.Portfolios.Count() + 1)));
            var newPortfolio = Mapper.Map<PortfolioDTO, Portfolio>(portfolio);
            if (db.Portfolios.CheckIfPortfolioExists(portfolio.Id))
            {
                db.Portfolios.UpdatePortfolioNameAndNotes(newPortfolio);               
            }
            else
            {
                newPortfolio.Customer = customerService.GetCustomerByProfileId(userId);
                db.Portfolios.Create(newPortfolio);
            }
            db.Save();
            return (newPortfolio.Id);
        }
        public void DeletePortfolio(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.PortfolioIdNotSet, "");
            if (!db.Portfolios.CheckIfPortfolioExists(id.Value))
                throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
            db.Portfolios.Delete(id.Value);
            db.Save();
        }

        public void CreatePortfolio(PortfolioDTO portfolio, string userId)
        {
            if (portfolio == null)
                throw new ValidationException(Resource.Resource.PortfolioNullReference, "");
            validateService.Validate(portfolio);
            Mapper.Initialize(cfg => cfg.CreateMap<PortfolioDTO, Portfolio>());
            Portfolio newPortfolio = Mapper.Map<PortfolioDTO, Portfolio>(portfolio);
            newPortfolio.Customer = customerService.GetCustomerByProfileId(userId);
            db.Portfolios.Create(newPortfolio);
            db.Save();
        }

        public void UpdatePortfolio(PortfolioDTO portfolio)
        {
            if (portfolio == null)
                throw new ValidationException(Resource.Resource.PortfolioNullReference, "");
            if (!db.Portfolios.CheckIfPortfolioExists(portfolio.Id))
                throw new ValidationException(Resource.Resource.PortfolioNotFound, "");
            validateService.Validate(portfolio);
            Mapper.Initialize(cfg => cfg.CreateMap<PortfolioDTO, Portfolio>());
            Portfolio newPortfolio = Mapper.Map<PortfolioDTO, Portfolio>(portfolio);
            db.Portfolios.Update(newPortfolio);
            db.Save();
        }

        public void UpdatePortfoliosDisplayIndex(Dictionary<string, string> portfolios)
        {
            foreach (var portfolio in portfolios)
            {
                db.Portfolios.ChangePortfolioDisplayIndex(Convert.ToInt32(portfolio.Key), Convert.ToInt32(portfolio.Value));
            }

        }
    }
}
