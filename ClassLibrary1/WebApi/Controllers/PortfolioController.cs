using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using BLL.DTO;
using BLL.Interfaces;
using log4net;
using WebApi.Models;
using Microsoft.AspNet.Identity;

namespace WebApi.Controllers
{
    public class PortfolioController : BaseController
    {
        private IPortfolioService portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            this.portfolioService = portfolioService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var portfolios = portfolioService.GetPortfoliosForUser(RequestContext.Principal.Identity.GetUserId());
            return Ok(Mapper.Map<IEnumerable<PortfolioDTO>, List<PortfolioModel>>(portfolios));
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            PortfolioModel portfolio;
            try
            {
                if (portfolioService.CheckAccess(RequestContext.Principal.Identity.GetUserId(), id))
                {
                    portfolio = Mapper.Map<PortfolioDTO, PortfolioModel>(portfolioService.GetPortfolio(id));
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

        [HttpPost]
        public IHttpActionResult Post([FromBody]PortfolioModel portfolio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                portfolioService.CreatePortfolio(Mapper.Map<PortfolioModel, PortfolioDTO>(portfolio), RequestContext.Principal.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update([FromBody]PortfolioModel portfolio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("ModelState");
            }
            try
            {
                if (portfolioService.CheckAccess(RequestContext.Principal.Identity.GetUserId(), portfolio.Id))
                {
                    portfolioService.UpdatePortfolio(Mapper.Map<PortfolioModel, PortfolioDTO>(portfolio), RequestContext.Principal.Identity.GetUserId());
                }
                else
                {
                    throw new Exception("You don't have access to portfolio with id: " + portfolio.Id);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok();
        }

        [Route("api/Portfolio/Update")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]int? id)
        {
            try
            {
                portfolioService.UpdatePortfolio(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (portfolioService.CheckAccess(RequestContext.Principal.Identity.GetUserId(), id))
                {
                    portfolioService.DeletePortfolio(id, RequestContext.Principal.Identity.GetUserId());
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
            return Ok();
        }

        [Route("api/portfolio/{id}/positions")]
        [HttpGet]
        public IHttpActionResult GetPortfolioPosition(int id)
        {
            var positions = new List<PositionModel>();
            try
            {
                if (portfolioService.CheckAccess(RequestContext.Principal.Identity.GetUserId(), id))
                {
                    positions = Mapper.Map<IEnumerable<PositionDTO>, List<PositionModel>>(portfolioService.GetPortfolioPositions(id));
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
            return Ok(positions);
        }
    }
}
