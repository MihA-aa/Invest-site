using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITransactionService
    {
        void Commit();
        void RollBack();
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}
