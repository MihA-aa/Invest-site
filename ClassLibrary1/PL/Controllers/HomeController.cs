using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;

namespace PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IUserService userService;

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }
        public ActionResult Index()
        {
            if (!userService.UserIsInRole("Employee", User.Identity.GetUserId()))
            {
                TempData["Message"] = "You were not assigned to any сustomer";
                return RedirectToAction("UnassignedUser", "Account");
            }
            return View();
        }
        
    }
}