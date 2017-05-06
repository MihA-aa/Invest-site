using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Enums;

namespace BLL.Interfaces
{
    public interface ICalculationService
    {
        decimal GetGain(decimal? curPrice, decimal? clPrice, decimal opPrice,
            int opWeight, decimal[] dividends, TradeTypes type);
        decimal GetAbsoluteGain(decimal? curPrice, decimal? clPrice, decimal opPrice,
            int opWeight, decimal[] dividends, TradeTypes type);
        decimal GetDividends(decimal[] dividends, int opWeight);
        decimal GetPortfolioValue(ICollection<decimal> positionsGain);
    }
}
