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
    public class ViewTemplateColumnController : BaseController
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PositionController));
        private IViewTemplateColumnService viewTemplateColumnService;

        public ViewTemplateColumnController(IViewTemplateColumnService viewTemplateColumnService)
        {
            this.viewTemplateColumnService = viewTemplateColumnService;
        }

        [HttpGet]
        public ActionResult RedactColumns(int? id)
        {
            if (id == null)
            {
                logger.Error("Id of ViewTemplate not set");
                return RedirectToAction("Http404", "Error");
            }
            return PartialView(id);
        }

        [HttpGet]
        public ActionResult Save(int? id)
        {
            ViewTemplateColumnModel viewTemplateColumn = null;
            try
            {
                if (id == 0)
                    return PartialView();
                var viewTemplateColumnDTO = viewTemplateColumnService.GetViewTemplateColumn(id);
                viewTemplateColumn = Mapper.Map<ViewTemplateColumnDTO, ViewTemplateColumnModel>(viewTemplateColumnDTO);
            }
            catch (ValidationException ex)
            {
                logger.Error(ex.ToString());
            }
            return PartialView(viewTemplateColumn);
        }

        [HttpPost]
        public ActionResult Save(ViewTemplateColumnModel viewTemplateColumn, int? templateId)
        {
            bool status = true;
            string message = "", property = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var viewTemplateColumnDTO = Mapper.Map<ViewTemplateColumnModel, ViewTemplateColumnDTO>(viewTemplateColumn);
                    viewTemplateColumnService.CreateOrUpdateViewTemplateColumn(viewTemplateColumnDTO, templateId);
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
                ViewBag.Id = viewTemplateColumnService.GetViewTemplateColumn(id).Id;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("Http500", "Error");
            }
            return PartialView();
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteViewTemplateColumn(int? id)
        {
            bool status = true;
            try
            {
                viewTemplateColumnService.DeleteViewTemplateColumn(id);
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