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
            var portfolios = portfolioService.GetPortfoliosForUser("1aaa023d-e950-47fc-9c3f-54fbffcc99cf"
                /*RequestContext.Principal.Identity.GetUserId()*/);
            return Ok(Mapper.Map<IEnumerable<PortfolioDTO>, List<PortfolioModel>>(portfolios));
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            PortfolioModel portfolio = new PortfolioModel();
            try
            {
                portfolio = Mapper.Map<PortfolioDTO, PortfolioModel>(portfolioService.GetPortfolio(id));
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
                portfolioService.CreatePortfolio(Mapper.Map<PortfolioModel, PortfolioDTO>(portfolio), "1aaa023d-e950-47fc-9c3f-54fbffcc99cf");
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
                return BadRequest(ModelState);
            }
            try
            {
                portfolioService.UpdatePortfolio(Mapper.Map<PortfolioModel, PortfolioDTO>(portfolio));
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
                portfolioService.DeletePortfolio(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetPortfolioPosition(int id)
        {
            var positions = new List<PositionModel>();
            try
            {
                positions = Mapper.Map<IEnumerable<PositionDTO>, List<PositionModel>>(portfolioService.GetPortfolioPositions(id));
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
