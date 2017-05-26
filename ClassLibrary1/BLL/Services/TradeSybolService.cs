using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    public class TradeSybolService: ITradeSybolService
    {
        IUnitOfWork db { get; }

        public TradeSybolService(IUnitOfWork uow)
        {
            db = uow;
        }
        public decimal GetPriceForDate(DateTime date, int symbolId)
        {
            return db.TradeSybols.GetPriceForDate(date, symbolId);
           
        }
    }
}
