using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Enums;
using DAL.Entities.Views;

namespace BLL.Interfaces
{
    public interface ITradeSybolService
    {
        decimal GetPriceForDate(DateTime date, int symbolId);

        TradeInforamation GetMaxGainForSymbolBetweenDate(DateTime dateFrom, DateTime dateTo, int symbolId,
            TradeTypesDTO type);
    }
}
