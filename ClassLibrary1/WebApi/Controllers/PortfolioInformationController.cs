using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class PortfolioInformationController : BaseController
    {
        private IPortfolioService portfolioService;

        public PortfolioInformationController(IPortfolioService portfolioService)
        {
            this.portfolioService = portfolioService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var portfolios = portfolioService.GetPortfoliosForUser(RequestContext.Principal.Identity.GetUserId());
            return Ok(Mapper.Map<IEnumerable<PortfolioDTO>, List<PortfolioInformationModel>>(portfolios));
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            PortfolioInformationModel portfolio;
            try
            {
                if (portfolioService.CheckAccess(RequestContext.Principal.Identity.GetUserId(), id))
                {
                    portfolio = Mapper.Map<PortfolioDTO, PortfolioInformationModel>(portfolioService.GetPortfolio(id));
                }
                else
                {
                    throw new Exception("You don't have access to portfolio with id: " + id);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok(portfolio);
        }
    }
}
