using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;

namespace PL.Util
{
    public class TransactionAttribute : ActionFilterAttribute
    {
        private ITransactionService transactionService;
        private IRecordService recordService;
        private string userId;
        private string queryPath;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            userId = HttpContext.Current.User.Identity.GetUserId();
            recordService = DependencyResolver.Current.GetService<IRecordService>();
            queryPath = HttpContext.Current.Request.Url.AbsolutePath;
            
            transactionService = DependencyResolver.Current.GetService<ITransactionService>();
            if (filterContext.Controller.ViewData.ModelState.IsValid && filterContext.HttpContext.Error == null)
                transactionService.BeginTransaction();

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            bool success;
            if (filterContext.Controller.ViewData.ModelState.IsValid && filterContext.HttpContext.Error == null)
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