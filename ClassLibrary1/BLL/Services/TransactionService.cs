using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    public class TransactionService : ITransactionService
    {
        protected IUnitOfWork db { get; }

        public TransactionService(IUnitOfWork uow)
        {
            db = uow;
        }

        public void Commit()
        {
            db.Commit();
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            db.BeginTransaction(isolationLevel);
        }

        public void RollBack()
        {
            db.RollBack();
        }
    }
}
