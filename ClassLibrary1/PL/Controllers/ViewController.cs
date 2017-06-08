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
    public class ViewController : BaseController
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PositionController));
        private IViewService viewService;
        private IViewTemplateService viewTemplateService;

        public ViewController(IViewService viewService, IViewTemplateService viewTemplateService)
        {
            this.viewService = viewService;
            this.viewTemplateService = viewTemplateService;
        }

        [HttpGet]
        public ActionResult Save(int? id)
        {
            ViewModel view = null;
            ViewBag.Templates = new SelectList(viewTemplateService.GetViewTemplates(), "Id", "Name");
            try
            {
                if (id == 0)
                    return PartialView();
                view = Mapper.Map<ViewDTO, ViewModel>(viewService.GetView(id));
            }
            catch (ValidationException ex)
            {
                logger.Error(ex.ToString());
            }
            return PartialView(view);
        }

        [HttpPost]
        public ActionResult Save(ViewModel view)
        {
            bool status = true;
            string message = "", property = "";
            if (ModelState.IsValid)
            {
                try
                {
                    viewService.CreateOrUpdateView(Mapper.Map<ViewModel, ViewDTO>(view));
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
                ViewBag.Id = viewService.GetView(id).Id;
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
        public ActionResult DeleteView(int? id)
        {
            bool status = true;
            try
            {
                viewService.DeleteView(id);
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