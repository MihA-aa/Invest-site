using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;

namespace PL.Controllers
{
    public class NavController : Controller
    {
        private IPortfolioService portfolioService;

        public NavController(IPortfolioService PortfolioService)
        {
            this.portfolioService = PortfolioService;
        }
        public PartialViewResult LeftMenu()
        {
            var portfolio = portfolioService.GetPortfolios();
            return PartialView(portfolio);
        }
    }
}