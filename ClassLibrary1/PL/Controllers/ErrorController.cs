using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ErrorController : Controller
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PositionController));
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