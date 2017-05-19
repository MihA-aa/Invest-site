﻿using System;
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
    public class NavController : Controller
    {
        private IPortfolioService portfolioService;

        public NavController(IPortfolioService PortfolioService)
        {
            this.portfolioService = PortfolioService;
        }
        public PartialViewResult LeftMenu()
        {
            var portfoliosDto = portfolioService.GetPortfolios().OrderBy(m => m.DisplayIndex);
            Mapper.Initialize(cfg => cfg.CreateMap<PortfolioDTO, PortfolioModel>());
            var portfolios =  Mapper.Map<IEnumerable<PortfolioDTO>, List<PortfolioModel>>(portfoliosDto);
            if (@TempData["PortfolioId"] == null)
                @TempData["PortfolioId"] = portfolios.First().Id;
            return PartialView(portfolios);
        }

        public ActionResult _General(int? id)
        {
            if(id == null)
                return PartialView();
            var portfolioDto = portfolioService.GetPortfolio(id);
            Mapper.Initialize(cfg => cfg.CreateMap<PortfolioDTO, PortfolioModel>());
            var portfolio = Mapper.Map<PortfolioDTO, PortfolioModel>(portfolioDto);
            TempData["PortfolioId"] = portfolioDto.Id;
            return PartialView(portfolio);
        }
        
        public ActionResult TradeManagementTable(int? id)
        {
            if (id == null)
                return Json(new { Success = false});
            var positionsDto = portfolioService.GetPortfolioPositions(id);
            Mapper.Initialize(cfg => cfg.CreateMap<PositionDTO, PositionModel>());
            var positions = Mapper.Map<IEnumerable<PositionDTO>, List<PositionModel>>(positionsDto);
            //positions.First().OpenDate =  new Date(parseInt(value.substr(6)));
            return Json(new { data = positions }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void RefreshPortfolioDisplayIndex(Dictionary<string, string> portfolios)
        {
            portfolioService.UpdatePortfoliosDisplayIndex(portfolios);
        }
    }
}