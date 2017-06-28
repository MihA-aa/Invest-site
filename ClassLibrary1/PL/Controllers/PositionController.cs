﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.DTO.Enums;
using BLL.Helpers;
using BLL.Interfaces;
using PL.Models;
using log4net;
using BLL.Infrastructure;
using Highsoft.Web.Mvc.Stocks;
using PL.Util;

namespace PL.Controllers
{
    public class PositionController : BaseController
    {
        private IPositionService positionService;

        public PositionController(IPositionService positionService)
        {
            this.positionService = positionService;
        }
        [HttpGet]
        public ActionResult Save(int? id)
        {
            PositionModel position = null;
            ViewBag.TradeTypeList = HelperService.GetSelectListFromEnum<TradeTypesDTO>();
            ViewBag.TradeStatusList = HelperService.GetSelectListFromEnum<TradeStatusesDTO>();
            try
            {
                if (id == 0)
                    return PartialView();
                position = Mapper.Map<PositionDTO, PositionModel>(positionService.GetPosition(id));
            }
            catch (ValidationException ex)
            {
                logger.Error(ex.ToString());
            }
            return PartialView(position);
        }

        [HttpPost]
        public ActionResult Save(PositionModel position, int? portfolioId)
        {
            bool status = false;
            string message="", property="";
            if (ModelState.IsValid)
            {
                status = true;
                try
                {
                    positionService.CreateOrUpdatePosition(Mapper.Map<PositionModel, PositionDTO>(position), portfolioId);
                }
                catch (ValidationException ex)
                {
                    logger.Error(ex.ToString());
                    status = false;
                    property = ex.Property;
                    message = ex.Message;
                }
                
            }
            return new JsonResult { Data = new { status = status, prop = property, message = message } };
        }
        
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                 ViewBag.Id = positionService.GetPosition(id).Id;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("Http404", "Error");
            }
            return PartialView();
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePosition(int? id)
        {
            bool status = true;
            try
            {
               positionService.DeletePosition(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }
        
        public ActionResult ChartOfGain([Bind(Prefix = "id")]int positionId)
        {
            var chart = positionService.GetChartForPosition(positionId);
            var newData = chart.Select(c => new LineSeriesData {X = c.Key, Y = (double) c.Value}).ToList();
            //newData.OrderByDescending(s=>s.Y).First().Marker.Symbol = "url(http://www.highcharts.com/demo/gfx/sun.png)";
            ViewBag.FlagsData = new List<FlagsSeriesData>
            {
                new FlagsSeriesData
                {
                    X = newData.OrderByDescending(s=>s.Y).First().X
                }
            };

            ViewData["sybolData"] = newData;
            ViewData["Position"] = positionService.GetPosition(positionId).Name;
            return View(ViewBag);
        }

        public double DateToUTC(DateTime theDate)
        {
            DateTime d1 = new DateTime(1970, 1, 1);
            DateTime d2 = theDate.ToUniversalTime();
            TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);

            return ts.TotalMilliseconds;
        }
    }
}