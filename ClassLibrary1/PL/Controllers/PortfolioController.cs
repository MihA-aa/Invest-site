using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using PL.Models;

namespace PL.Controllers
{
    public class PortfolioController : BaseController
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PositionController));
        private IPortfolioService portfolioService;

        public PortfolioController(IPortfolioService PortfolioService)
        {
            this.portfolioService = PortfolioService;
        }
        public PartialViewResult CreatePortfolio()
        {
            return PartialView("_General");
        }

        [HttpPost]
        public ActionResult CreateUpdatePortfolio(PortfolioModel portfolioModel)
        {
            var portfolioDto = Mapper.Map<PortfolioModel, PortfolioDTO>(portfolioModel);
            TempData["PortfolioId"] = portfolioService.CreateOrUpdatePortfolio(portfolioDto, User.Identity.GetUserId());
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult DeletePortfolio(int id)
        {
            try
            {
                portfolioService.DeletePortfolio(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return Json("Response from Delete");
        }
    }
}