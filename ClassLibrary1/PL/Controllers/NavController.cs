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
        private ISymbolViewService symbolViewService;
        private ITradeSybolService tradeSybolService;
        private IViewTemplateService viewTemplateService;
        private IViewTemplateColumnService viewTemplateColumnService;

        public NavController(IPortfolioService portfolioService, ISymbolViewService symbolViewService,
            ITradeSybolService tradeSybolService, IViewTemplateService viewTemplateService, IViewTemplateColumnService viewTemplateColumnService)
        {
            this.portfolioService = portfolioService;
            this.symbolViewService = symbolViewService;
            this.tradeSybolService = tradeSybolService;
            this.viewTemplateService = viewTemplateService;
            this.viewTemplateColumnService = viewTemplateColumnService;
        }
        public PartialViewResult LeftMenu()
        {
            var portfoliosDto = portfolioService.GetPortfolios().OrderBy(m => m.DisplayIndex);
            Mapper.Initialize(cfg => cfg.CreateMap<PortfolioDTO, PortfolioModel>());
            var portfolios =  Mapper.Map<IEnumerable<PortfolioDTO>, List<PortfolioModel>>(portfoliosDto);
            if (@TempData["PortfolioId"] == null && portfolios.Any())
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

        [HttpPost]
        public ActionResult LoadViewTampleteData()
        {
            var draw = Request.Form?.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int totalRecords = 0;

            var viewTemplatesDto = viewTemplateService.GetViewTemplates();
            
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                viewTemplatesDto = viewTemplatesDto.OrderBy(sortColumn + " " + sortColumnDir);
            }

            totalRecords = viewTemplatesDto.Count();
            viewTemplatesDto = viewTemplatesDto.Skip(skip).Take(pageSize).ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<ViewTemplateDTO, ViewTemplateModel>());
            var data = Mapper.Map<IEnumerable<ViewTemplateDTO>, List<ViewTemplateModel>>(viewTemplatesDto);
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = data }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult LoadViewColumnTampleteData(int? id)
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

            var viewTemplateColumnsDto = viewTemplateService.GetViewTemplateColumns(id.Value);

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                viewTemplateColumnsDto = viewTemplateColumnsDto.OrderBy(sortColumn + " " + sortColumnDir);
            }

            totalRecords = viewTemplateColumnsDto.Count();
            viewTemplateColumnsDto = viewTemplateColumnsDto.Skip(skip).Take(pageSize).ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<ViewTemplateColumnDTO, ViewTemplateColumnModel>());
            var data = Mapper.Map<IEnumerable<ViewTemplateColumnDTO>, List<ViewTemplateColumnModel>>(viewTemplateColumnsDto);
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutocompleteSymbolSearch(string term)
        {
            var symbols = symbolViewService.SearchSymbolsByName(term);
            return Json(symbols, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetNameByTemplateId(int? id)
        {
            var name = viewTemplateService.GetNameByTemplateId(id);
            return Json(new { templateName = name});
        }

        [HttpPost]
        public ActionResult GetFormatsForColumn(string column)
        {
            var columnFormatsList = viewTemplateColumnService.GetFormatsByColumnName(column);
            return Json(new { columnFormats = columnFormatsList});
        }
        
        [HttpPost]
        public ActionResult CheckIfExist(string value)
        {
            var symbol = symbolViewService.GetSymbolViewByName(value);
            var isFound = symbol != null;
            var name = "";
            var id = 0;
            if (symbol != null)
            {
                name = symbol.Name;
                id = symbol.SymbolID;
            }
            return Json(new { success = isFound, symbolname = name, symbolId = id });
        }

        [HttpPost]
        public ActionResult GetSybolPriceForDate(DateTime date, int symbolId)
        {
            var symbolprice = tradeSybolService.GetPriceForDate(date, symbolId);
            return Json(new { success = true, price = symbolprice});
        }

        [HttpPost]
        public void RefreshPortfolioDisplayIndex(Dictionary<string, string> portfolios)
        {
            portfolioService.UpdatePortfoliosDisplayIndex(portfolios);
        }
    }
}