using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interfaces;

namespace PL.Controllers
{
    public class ErrorController :BaseController
    {
        public ErrorController()
        {
            LogManager.GetLogger(Type.GetType("PL.Controllers." + this.GetType().Name));
        }
        public ActionResult General(Exception exception)
        {
            logger.Error(exception.ToString());
            return View(exception);
        }

        public ActionResult Http404()
        {
            logger.Error(Request.Url.AbsoluteUri+" not found");
            return View();
        }

        public ActionResult Http403()
        {
            logger.Error(Request.Url.AbsoluteUri + " Access restricted");
            return View();
        }
        public ActionResult Http500()
        {
            logger.Error(Request.Url.AbsoluteUri + " Internal server error");
            return View();
        }

        public ActionResult ExhibitNotFound()
        {
            logger.Error(Request.Url.AbsoluteUri + " Exhibit Not Found");
            return View();
        }
    }
}