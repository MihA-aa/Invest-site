﻿using System;
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
    public class ViewController : BaseController
    {
        private IViewService viewService;
        private IViewTemplateService viewTemplateService;

        public static ITransactionService tsss;
        
        public ViewController(IViewService viewService, IViewTemplateService viewTemplateService)
        {
            this.viewService = viewService;
            this.viewTemplateService = viewTemplateService;
        }

        [HttpGet]
        public ActionResult Save(int? id)
        {
            ViewModel view = null;
            ViewBag.DateFormatsList = HelperService.GetSelectListFromEnum<DateFormatsDTO>();
            ViewBag.Templates = new SelectList(viewTemplateService.GetViewTemplatesForUser(User.Identity.GetUserId()), "Id", "Name");
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

        [HttpPost, Transaction]
        public ActionResult Save(ViewModel view)
        {
            bool status = true;
            string message = "", property = "";
            if (ModelState.IsValid)
            {
                try
                {
                    viewService.CreateOrUpdateView(Mapper.Map<ViewModel, ViewDTO>(view), User.Identity.GetUserId());
                }
                catch (ValidationException ex)
                {
                    logger.Error(ex.ToString());
                    ModelState.AddModelError("", ex.Message);
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
        [ActionName("Delete"), Transaction]
        public ActionResult DeleteView(int? id)
        {
            bool status = true;
            try
            {
                viewService.DeleteView(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                logger.Error(ex.ToString());
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}