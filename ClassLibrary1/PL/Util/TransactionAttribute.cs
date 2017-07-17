using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using BLL.Services;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Cfg;
using System.Runtime.Remoting.Messaging;
using DALEF.NHibernateHelp;

namespace PL.Util
{
    public class TransactionAttribute : ActionFilterAttribute
    {
        private ITransactionService transactionService;
        private IRecordService recordService;
        //private readonly ITransaction _currentTransaction;

        public TransactionAttribute()
        {
            //transactionService = DependencyResolver.Current.GetService<ITransactionService>();
             //recordService = DependencyResolver.Current.GetService<IRecordService>();
            //_currentTransaction = NHibernateSessionFactory.getSession("Data Source=ERMOLAEVM;Initial Catalog=FuckingDb; Integrated Security=True;MultipleActiveResultSets=True;").Transaction;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //_currentTransaction.Begin();
            transactionService = DependencyResolver.Current.GetService<ITransactionService>();
            if (filterContext.Controller.ViewData.ModelState.IsValid && filterContext.HttpContext.Error == null)
                transactionService.BeginTransaction();

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //if (_currentTransaction.IsActive)
            //{
            //    if (filterContext.Exception == null)
            //    {
            //        _currentTransaction.Commit();
            //    }
            //    else
            //    {
            //        _currentTransaction.Rollback();
            //    }
            //}

            if (filterContext.Controller.ViewData.ModelState.IsValid && filterContext.HttpContext.Error == null)
                transactionService.Commit();
            else
                transactionService.RollBack();

            base.OnActionExecuted(filterContext);
        }
    }
}