using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        [HandleError()]
        public ActionResult Index()
        {
            //throw new ArgumentException("Специальная ошибка для теста");
            return View();
        }
        
    }
}