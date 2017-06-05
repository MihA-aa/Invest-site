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
                var viewTemplateDTO = viewTemplateService.GetViewTemplate(id);
                Mapper.Initialize(cfg => cfg.CreateMap<ViewTemplateDTO, ViewTemplateModel>());
                viewTemplate = Mapper.Map<ViewTemplateDTO, ViewTemplateModel>(viewTemplateDTO);
                viewTemplate.Columns = new SelectList(viewTemplateService.GetViewTemplateColumns(id), "Id", "Name");
            }
            catch (ValidationException ex)
            {
                logger.Error(ex.ToString());
            }
            return PartialView(viewTemplate);
        }

        [HttpPost]
        public ActionResult Save(ViewTemplateModel viewTemplate)
        {
            bool status = true;
            string message = "", property = "";
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<ViewTemplateModel, ViewTemplateDTO>());
                    var viewTemplateDTO = Mapper.Map<ViewTemplateModel, ViewTemplateDTO>(viewTemplate);
                    viewTemplateService.CreateOrUpdateViewTemplate(viewTemplateDTO);
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
                ViewBag.Id = viewTemplateService.GetViewTemplate(id).Id;
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
        public ActionResult DeleteViewTemplate(int? id)
        {
            bool status = true;
            try
            {
                viewTemplateService.DeleteViewTemplate(id);
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