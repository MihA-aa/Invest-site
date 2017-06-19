using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.DTO.Enums;
using BLL.Interfaces;
using DAL.Entities.Views;
using DAL.Interfaces;

namespace BLL.Services
{
    public class TradeSybolService: BaseService, ITradeSybolService
    {
        public TradeSybolService(IUnitOfWork uow, IValidateService vd, IMapper map) : base(uow, vd, map) { }
        public decimal GetPriceForDate(DateTime date, int symbolId)
        {
            return db.TradeSybols.GetPriceForDate(date, symbolId);
        }

        public TradeSybolViewDTO GetPriceAndDateLastUpdate(int symbolId)
        {
            return IMapper.Map<TradeSybolView, TradeSybolViewDTO>(db.TradeSybols.GetPriceAndDateLastUpdate(symbolId));
        }

        public TradeInforamation GetMaxGainForSymbolBetweenDate(DateTime dateFrom, DateTime dateTo, int symbolId, TradeTypesDTO type)
        {
            var tradeDates = db.TradeSybols.GetMaxDateForGainForSymbol(dateFrom, dateTo, symbolId);
            if (type == TradeTypesDTO.Long)
                return tradeDates.FirstOrDefault();
            else
                return tradeDates.Skip(1).FirstOrDefault();
        }
    }
}
