﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using PL.Models;
using PL.Util;
using System.Linq.Dynamic;
using System.Web.WebPages;
using BLL.DTO.Enums;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace PL.Controllers
{
    public class NavController : BaseController
    {
        private IPortfolioService portfolioService;
        private ISymbolViewService symbolViewService;
        private ITradeSybolService tradeSybolService;
        private IViewTemplateService viewTemplateService;
        private IViewTemplateColumnService viewTemplateColumnService;
        private IViewService viewService;

        public NavController(IPortfolioService portfolioService, ISymbolViewService symbolViewService,
            ITradeSybolService tradeSybolService, IViewTemplateService viewTemplateService, 
            IViewTemplateColumnService viewTemplateColumnService, IViewService viewService)
        {
            this.portfolioService = portfolioService;
            this.symbolViewService = symbolViewService;
            this.tradeSybolService = tradeSybolService;
            this.viewTemplateService = viewTemplateService;
            this.viewTemplateColumnService = viewTemplateColumnService;
            this.viewService = viewService;
        }
        public PartialViewResult LeftMenu()
        {
            var portfoliosDto = portfolioService.GetPortfoliosForUser(User.Identity.GetUserId()).OrderBy(m => m.DisplayIndex);
            var portfolios =  Mapper.Map<IEnumerable<PortfolioDTO>, List<PortfolioModel>>(portfoliosDto);
            if (@TempData["PortfolioId"] == null && portfolios.Any())
                @TempData["PortfolioId"] = portfolios.First().Id;
            return PartialView(portfolios);
        }

        public PartialViewResult ViewList()
        {
            var viewsDto = viewService.GetViewsForUser(User.Identity.GetUserId()).OrderBy(v => v.Id);
            var views = Mapper.Map<IEnumerable<ViewDTO>, List<ViewModel>>(viewsDto);
            return PartialView(views);
        }

        public ActionResult _General(int? id)
        {
            if(id == null)
                return PartialView();
            var portfolioDto = portfolioService.GetPortfolio(id);
            var portfolio = Mapper.Map<PortfolioDTO, PortfolioModel>(portfolioDto);
            TempData["PortfolioId"] = portfolioDto.Id;
            return PartialView(portfolio);
        }

        [HttpPost]
        public ActionResult ApplyView(int? id)
        {
            var view = viewService.GetView(id ?? 1);
            var viewTemplateColumns = viewTemplateService.GetViewTemplateColumns(view.ViewTemplateId)
                .OrderBy(c => c.DisplayIndex);
            var viewTemplate = viewTemplateService.GetViewTemplate(view.ViewTemplateId);
            int sortColumnDisplayIndex =viewTemplateColumns.Where(c => c.Id == viewTemplate.SortColumnId)
                .Select(c => c.DisplayIndex)
                .FirstOrDefault();
            var sortOrder = (SortingDTO) viewTemplate.SortOrder;
            var tradeStatus = (TradeStatusesDTO)viewTemplate.Positions;
            var columns = new List<dynamic>
           {
                new {title = "", data = "CurrencySymbol", name = "CurrencySymbol", autoWidth = "false", width = "10px", render = "", className = "hide_column"},
                new {title = "", data = "Id", name = "Id", autoWidth = "false", width = "10px", render = "saveActionLink"},
                new {title = "", data = "Id", name = "Id", autoWidth = "false", width = "10px", render = "deleteActionLink"}
            };

            foreach(var column in viewTemplateColumns)
            {
                columns.Add(new
                {
                    title = column.Name,
                    data = column.ColumnName.Replace(" ", string.Empty),
                    name = column.ColumnName.Replace(" ", string.Empty),
                    render = column.ColumnName.Replace(" ", string.Empty),
                    format = column.FormatName,
                    autoWidth = "true"
                });
            }
            return Json(new { columns, dateFormat = view.DateFormat,moneyPrecision = view.MoneyPrecision,
            percentyPrecision = view.PercentyPrecision, showPosition = (int)tradeStatus == 3 ? "All": tradeStatus.ToString(),
                sortOrder = sortOrder.ToString(), sortColumnDisplayIndex = sortColumnDisplayIndex+2
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LoadData(int? id)
        {
            if (id == null)
                return Json(new { draw = 0, recordsFiltered = 0, recordsTotal = 0, data = "" }, JsonRequestBehavior.AllowGet);

            var draw = Request.Form?.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            var defaultSearchStatus = Request.Form.GetValues("search[value]").FirstOrDefault();
            if (defaultSearchStatus == "All")
                defaultSearchStatus = "";


            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault().Replace(" ", ""); ;
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            var searchColumnsList = new Dictionary<string, string>();

            for (int i = 0; i < (Request.Form.Count - 9)/6; i++)
            {
                if (!searchColumnsList.ContainsKey(Request.Form?.GetValues("columns[" + i + "][data]").FirstOrDefault()))
                    searchColumnsList.Add(Request.Form?.GetValues("columns[" + i + "][data]").FirstOrDefault(), Request.Form?.GetValues("columns["+i+"][search][value]").FirstOrDefault());
            }
            
            var symbolName = searchColumnsList.ContainsKey("SymbolName")? searchColumnsList["SymbolName"] : "";
            var status = searchColumnsList.ContainsKey("TradeStatus") ? searchColumnsList["TradeStatus"] : "";
            var openDateFrom = searchColumnsList.ContainsKey("OpenDate") && searchColumnsList["OpenDate"].Contains('t') ? searchColumnsList["OpenDate"].Split('t')[0] : "";
            var openDateTo = searchColumnsList.ContainsKey("OpenDate") && searchColumnsList["OpenDate"].Contains('t') ? searchColumnsList["OpenDate"].Split('t')[1] : "";
            var closeDateFrom = searchColumnsList.ContainsKey("CloseDate") && searchColumnsList["CloseDate"].Contains('t') ? searchColumnsList["CloseDate"].Split('t')[0] : "";
            var closeDateTo = searchColumnsList.ContainsKey("CloseDate") && searchColumnsList["CloseDate"].Contains('t') ? searchColumnsList["CloseDate"].Split('t')[1] : "";

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
            if (string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(defaultSearchStatus))
            {
                positionsDto = positionsDto.Where(m => m.TradeStatus.ToString() == defaultSearchStatus);
            }
            if (status == "All")
            {
                status = "";
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
            var data = Mapper.Map<IEnumerable<PositionDTO>, List<PositionModel>>(positionsDto);
            foreach (var pos in data)
                pos.CurrencySymbol = symbolViewService.GetCurrencySymbolViewBySymbolId(pos.SymbolId);
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

            var viewTemplatesDto = viewTemplateService.GetViewTemplatesForUser(User.Identity.GetUserId());
            
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                viewTemplatesDto = viewTemplatesDto.OrderBy(sortColumn + " " + sortColumnDir);
            }

            totalRecords = viewTemplatesDto.Count();
            viewTemplatesDto = viewTemplatesDto.Skip(skip).Take(pageSize).ToList();
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
            var data = Mapper.Map<IEnumerable<ViewTemplateColumnDTO>, List<ViewTemplateColumnModel>>(viewTemplateColumnsDto);
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LoadViewData()
        {
            var draw = Request.Form?.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int totalRecords = 0;

            var viewsDto = viewService.GetViewsForUser(User.Identity.GetUserId());

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                viewsDto = viewsDto.OrderBy(sortColumn + " " + sortColumnDir);
            }

            totalRecords = viewsDto.Count();
            viewsDto = viewsDto.Skip(skip).Take(pageSize).ToList();
            var data = Mapper.Map<IEnumerable<ViewDTO>, List<ViewModel>>(viewsDto);
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
            if (isFound)
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

        [HttpPost, Transaction]
        public void RefreshPortfolioDisplayIndex(Dictionary<string, string> portfolios)
        {
            portfolioService.UpdatePortfoliosDisplayIndex(portfolios);
        }

        [HttpPost, Transaction]
        public void UpdateColumnOrder(int id, int fromPosition, int toPosition, string direction)
        {
            viewTemplateColumnService.UpdateColumnOrder(id, fromPosition, toPosition, direction);
        }
    }
}