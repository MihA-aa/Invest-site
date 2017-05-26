using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Enums;
using DAL.Entities;
using DAL.Enums;

namespace BLL.Interfaces
{
    public interface ICalculationService
    {
        decimal GetGain(decimal? curPrice, decimal? clPrice, decimal opPrice,
            int opWeight, decimal dividends, TradeTypesDTO type);
        decimal GetAbsoluteGain(decimal? curPrice, decimal? clPrice, decimal opPrice,
            int opWeight, decimal dividends, TradeTypesDTO type);
        decimal GetDividends(decimal dividends, int opWeight);
        decimal GetPortfolioValue(ICollection<decimal> positionsGain);
    }
}
