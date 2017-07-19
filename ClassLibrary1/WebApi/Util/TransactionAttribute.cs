using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace WebApi.Util
{
    public class TransactionAttribute : ActionFilterAttribute
    {
        private ITransactionService transactionService;
        private IRecordService recordService;
        private string userId;
        private string queryPath;

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            userId = HttpContext.Current.User.Identity.GetUserId();
            recordService = DependencyResolver.Current.GetService<IRecordService>();
            queryPath = HttpContext.Current.Request.Url.AbsolutePath;

            //var controller = filterContext.Request.GetRouteData().Values["controller"];
            //var controller = filterContext.ControllerContext.Controller;

            transactionService = DependencyResolver.Current.GetService<ITransactionService>();
            if (filterContext.ModelState.IsValid)
                transactionService.BeginTransaction();

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            bool success;
            if (filterContext.ActionContext.ModelState.IsValid)
            {
                success = true;
                transactionService.Commit();
            }
            else
            {
                success = false;
                transactionService.RollBack();
            }

            recordService.CreateRecord(queryPath, userId, success);

            base.OnActionExecuted(filterContext);
        }
    }
}