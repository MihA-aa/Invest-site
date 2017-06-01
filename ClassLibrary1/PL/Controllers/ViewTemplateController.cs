using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using PL.Models;
using BLL.Helpers;

namespace PL.Controllers
{
    public class ViewTemplateController : Controller
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PositionController));
        private IViewTemplateService viewTemplateService;

        public ViewTemplateController(IViewTemplateService viewTemplateService)
        {
            this.viewTemplateService = viewTemplateService;
        }
        [HttpGet]
        public ActionResult Save(int? id)
        {
            ViewTemplateModel viewTemplate = null;
            try
            {
                if (id == 0)
                    return PartialView();
                var positionDto = viewTemplateService.GetViewTemplate(id);
                Mapper.Initialize(cfg => cfg.CreateMap<ViewTemplateDTO, ViewTemplateModel>());
                viewTemplate = Mapper.Map<ViewTemplateDTO, ViewTemplateModel>(positionDto);
            }
            catch (ValidationException ex)
            {
                logger.Error(ex.ToString());
            }
            return PartialView(viewTemplate);
        }

        [HttpPost]
        public ActionResult Save(ViewTemplateModel viewTemplate, int? viewTampleteId)
        {
            //bool status = true;
            //string message = "", property = "";
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        Mapper.Initialize(cfg => cfg.CreateMap<PositionModel, PositionDTO>());
            //        var positionDto = Mapper.Map<PositionModel, PositionDTO>(position);
            //        positionService.CreateOrUpdatePosition(positionDto, portfolioId);
            //    }
            //    catch (ValidationException ex)
            //    {
            //        logger.Error(ex.ToString());
            //        status = false;
            //        property = ex.Property;
            //        message = ex.Message;
            //    }

            //}
            //return new JsonResult { Data = new { status = status, prop = property, message = message } };

            return new JsonResult { Data = new { status = true } };
        }

    }
}