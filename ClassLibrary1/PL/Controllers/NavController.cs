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
using System.Web.WebPages;

namespace PL.Controllers
{
    public class NavController : Controller
    {
        private IPortfolioService portfolioService;
        private ISymbolService symbolService;

        public NavController(IPortfolioService PortfolioService, ISymbolService symbolService)
        {
            this.portfolioService = PortfolioService;
            this.symbolService = symbolService;
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

            var symbolName = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
            var status = Request.Form.GetValues("columns[8][search][value]").FirstOrDefault();
            var openDateFrom = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault();
            var openDateTo = Request.Form.GetValues("columns[6][search][value]").FirstOrDefault();
            var closeDateFrom = Request.Form.GetValues("columns[10][search][value]").FirstOrDefault();
            var closeDateTo = Request.Form.GetValues("columns[11][search][value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int totalRecords = 0;

            var positionsDto = portfolioService.GetPortfolioPositions(id);

            #region Searching and sorting 
            if (!string.IsNullOrEmpty(openDateFrom))
            {
                positionsDto = positionsDto.Where(p => p.OpenDate.Date >= openDateFrom.AsDateTime());
            }
            if (!string.IsNullOrEmpty(openDateTo))
            {
                positionsDto = positionsDto.Where(p => p.OpenDate.Date <= openDateTo.AsDateTime());
            }
            if (!string.IsNullOrEmpty(closeDateFrom))
            {
                positionsDto = positionsDto.Where(p => p.CloseDate != null && p.CloseDate.Value.Date >= closeDateFrom.AsDateTime());
            }
            if (!string.IsNullOrEmpty(closeDateTo))
            {
                positionsDto = positionsDto.Where(p => p.CloseDate != null && p.CloseDate.Value.Date <= closeDateTo.AsDateTime());
            }
            if (!string.IsNullOrEmpty(symbolName))
            {
                positionsDto = positionsDto.Where(a => a.SymbolName.Contains(symbolName));
            }
            if (!string.IsNullOrEmpty(status))
            {
                positionsDto = positionsDto.Where(m => m.TradeStatus.ToString() == status);
            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                positionsDto = positionsDto.OrderBy(sortColumn + " " + sortColumnDir);
            }
            #endregion

            totalRecords = positionsDto.Count();
            positionsDto = positionsDto.Skip(skip).Take(pageSize).ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<PositionDTO, PositionModel>());
            var data = Mapper.Map<IEnumerable<PositionDTO>, List<PositionModel>>(positionsDto);
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = data }, JsonRequestBehavior.AllowGet);
                
        }


        public ActionResult AutocompleteSymbolSearch(string term)
        {
            var symbols = symbolService.SearchSymbolsByName(term);
            return Json(symbols, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CheckIfExist(string value)
        {
            var symbol = symbolService.GetSymbolByName(value);
            var isFound = symbol != null;
            return Json(new { success = isFound });
        }


        [HttpPost]
        public void RefreshPortfolioDisplayIndex(Dictionary<string, string> portfolios)
        {
            portfolioService.UpdatePortfoliosDisplayIndex(portfolios);
        }
    }
}