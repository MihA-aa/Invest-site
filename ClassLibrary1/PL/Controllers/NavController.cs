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
            var portfolios = portfolioService.GetPortfolios().OrderBy(m=>m.DisplayIndex);
            if (@TempData["PortfolioId"] == null)
                @TempData["PortfolioId"] = portfolios.First().Id;
            return PartialView(portfolios);
        }

        [HttpPost]
        public void RefreshPortfolioDisplayIndex(Dictionary<string, string> portfolios)
        {
            portfolioService.UpdatePortfoliosDisplayIndex(portfolios);
        }
    }
}