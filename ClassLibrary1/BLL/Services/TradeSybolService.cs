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

        public TradeSybolViewDTO GetPriceAndDateLastUpdate(int symbolId)
        {
            var info = db.TradeSybols.GetPriceAndDateLastUpdate(symbolId);
            if (info == null)
            {
                return null;
            }
            Mapper.Initialize(cfg => cfg.CreateMap<TradeSybolView, TradeSybolViewDTO>());
            return Mapper.Map<TradeSybolView, TradeSybolViewDTO>(info);
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
