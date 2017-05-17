using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using PL.Models;

namespace PL.Controllers
{
    public class PortfolioController : Controller
    {
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
        public ActionResult CreatePortfolio(PortfolioModel portfolioModel)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<PortfolioModel, PortfolioDTO>());
            PortfolioDTO portfolioDto = Mapper.Map<PortfolioModel, PortfolioDTO>(portfolioModel);
            TempData["PortfolioId"] = portfolioService.CreatePortfolio(portfolioDto);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult DeletePortfolio(int id)
        {
            portfolioService.DeletePortfolio(id);
            return Json("Response from Delete");
        }

        //[HttpDelete]
        //public JsonResult DeletePortfolio(int id)
        //{
        //    portfolioService.DeletePortfolio(id);
        //    return Json("Response from Delete");
        //}
    }
}