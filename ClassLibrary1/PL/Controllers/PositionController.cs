using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Helpers;
using BLL.Interfaces;
using PL.Models;
using BLL.Infrastructure;

namespace PL.Controllers
{
    public class PositionController : Controller
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PositionController));
        private IPositionService positionService;

        public PositionController(IPositionService positionService)
        {
            this.positionService = positionService;
        }
        [HttpGet]
        public ActionResult Save(int? id)
        {
            PositionModel position = null;
            try
            {
                if (id == 0)
                    return PartialView();
                var positionDto = positionService.GetPosition(id);
                Mapper.Initialize(cfg => cfg.CreateMap<PositionDTO, PositionModel>());
                position = Mapper.Map<PositionDTO, PositionModel>(positionDto);
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
            bool status = true;
            string message="", property="";
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<PositionModel, PositionDTO>());
                    var positionDto = Mapper.Map<PositionModel, PositionDTO>(position);
                    positionService.CreateOrUpdatePosition(positionDto, portfolioId);
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
                return HttpNotFound();
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
    }
}