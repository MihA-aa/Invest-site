using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.DTO.Enums;
using BLL.Interfaces;
using PL.Models;
using BLL.Helpers;
using log4net;
using Microsoft.AspNet.Identity;
using PL.Util;

namespace PL.Controllers
{
    public class ViewTemplateController : BaseController
    {
        private IViewTemplateService viewTemplateService;

        public ViewTemplateController(IViewTemplateService viewTemplateService)
        {
            this.viewTemplateService = viewTemplateService;
        }

        [HttpGet]
        public ActionResult Save(int? id)
        {
            ViewTemplateModel viewTemplate = null;
            ViewBag.ShowPositionsList = HelperService.GetSelectListFromEnum<TemplatePositionsDTO>();
            ViewBag.SortingList = HelperService.GetSelectListFromEnum<SortingDTO>();
            try
            {
                if (id == 0)
                    return PartialView();
                viewTemplate = Mapper.Map<ViewTemplateDTO, ViewTemplateModel>(viewTemplateService.GetViewTemplate(id));
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
                    viewTemplateService.CreateOrUpdateViewTemplate(Mapper.Map<ViewTemplateModel, ViewTemplateDTO>(viewTemplate), User.Identity.GetUserId());
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
                viewTemplateService.DeleteViewTemplate(id, User.Identity.GetUserId());
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