using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Enums;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Enums;

namespace BLL.Services
{
    public class CalculationService : ICalculationService
    {
        public decimal GetGain(decimal? curPrice, decimal? clPrice, decimal opPrice,
            int opWeight, decimal dividends, TradeTypesDTO type)
        {
            decimal gain;
            if (opPrice == 0 || opWeight == 0)
            {
                gain = 0;
            }
            else
            {
                var absGain = GetAbsoluteGain(curPrice, clPrice, opPrice, opWeight, dividends, type);
                gain = absGain/(opPrice*opWeight);
            }
            return gain;
        }

        public virtual decimal GetAbsoluteGain(decimal? curPrice, decimal? clPrice, decimal opPrice, 
            int opWeight, decimal dividends, TradeTypesDTO type)
        {
            var usePrice = curPrice ?? clPrice.Value;
            decimal absGain;
            if (type == TradeTypesDTO.Long)
            {
                absGain = (usePrice + dividends - opPrice)*opWeight;
            }
            else
            {
                absGain = (opPrice - dividends - usePrice) * opWeight;
            }
            return absGain;
        }
        
        public decimal GetDividends(decimal dividends, int opWeight)
        {
            return dividends * opWeight;
        }
    }
}
