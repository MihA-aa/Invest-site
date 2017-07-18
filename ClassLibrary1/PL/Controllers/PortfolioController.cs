using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Helpers;
using Microsoft.AspNet.Identity;
using PL.Models;
using PL.Util;
using log4net;

namespace PL.Controllers
{
    public class PortfolioController : BaseController
    {
        private IPortfolioService portfolioService;

        public PortfolioController(IPortfolioService PortfolioService, ITransactionService ts) : base(ts)
        {
            this.portfolioService = PortfolioService;
        }
        public PartialViewResult CreatePortfolio()
        {
            return PartialView("_General");
        }

        [HttpPost, Transaction]
        public ActionResult Save(PortfolioModel portfolioModel)
        {
            try
            { 
                var portfolioDto = Mapper.Map<PortfolioModel, PortfolioDTO>(portfolioModel);
                TempData["PortfolioId"] = portfolioService.CreateOrUpdatePortfolio(portfolioDto, User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                logger.Error(ex.ToString());
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost, Transaction]
        [ActionName("Delete")]
        public ActionResult Delete(int? id)
        {
            bool status = true;
            try
            {
                portfolioService.DeletePortfolio(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                logger.Error(ex.ToString());
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}