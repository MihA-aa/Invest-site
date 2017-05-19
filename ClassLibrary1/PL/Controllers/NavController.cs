using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using PL.Models;
using System.Linq.Dynamic;

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
        
        [HttpPost]
        public ActionResult LoadData(int? id)
        {
            if (id == null)
                return Json(new { Success = false });

            var draw = Request.Form?.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int totalRecords = 0;

            var positionsDto = portfolioService.GetPortfolioPositions(id);
            
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                positionsDto = positionsDto.OrderBy(sortColumn + " " + sortColumnDir);
            }

            totalRecords = positionsDto.Count();
            positionsDto = positionsDto.Skip(skip).Take(pageSize).ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<PositionDTO, PositionModel>());
            var data = Mapper.Map<IEnumerable<PositionDTO>, List<PositionModel>>(positionsDto);
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = data }, JsonRequestBehavior.AllowGet);
                
        }


        [HttpPost]
        public void RefreshPortfolioDisplayIndex(Dictionary<string, string> portfolios)
        {
            portfolioService.UpdatePortfoliosDisplayIndex(portfolios);
        }
    }
}