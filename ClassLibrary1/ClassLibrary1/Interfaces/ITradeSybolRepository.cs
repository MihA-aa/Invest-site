using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Views;

namespace DAL.Interfaces
{
    public interface ITradeSybolRepository
    {
        decimal GetPriceForDate(DateTime date, int symbolId);
        IEnumerable<TradeInforamation> GetMaxDateForGainForSymbol(DateTime dateFrom, DateTime dateTo, int symbolId);
        TradeSybolView GetPriceAndDateLastUpdate(int symbolId);
    }
}
