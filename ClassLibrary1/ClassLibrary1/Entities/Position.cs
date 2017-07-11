using System;
using DAL.Enums;

namespace DAL.Entities
{
    public class Position
    {
        public virtual int Id { get; set; }
        public virtual int SymbolId { get; set; }
        public virtual Symbols SymbolType { get; set; }
        public virtual string SymbolName { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime OpenDate  { get; set; }
        public virtual decimal OpenPrice { get; set; }
        public virtual int OpenWeight { get; set; }
        public virtual TradeTypes TradeType  { get; set; }
        public virtual TradeStatuses TradeStatus  { get; set; }
        public virtual decimal Dividends { get; set; }
        public virtual DateTime? CloseDate { get; set; }
        public virtual decimal? ClosePrice { get; set; }
        public virtual decimal? CurrentPrice { get; set; }
        public virtual DateTime? LastUpdateDate { get; set; }
        public virtual decimal? LastUpdatePrice { get; set; }
        public virtual decimal Gain { get; set; }
        public virtual decimal AbsoluteGain { get; set; }
        public virtual decimal MaxGain { get; set; }
        
        public virtual Portfolio Portfolio { get; set; }

        public object Clone()
        {
            return new Position
            {
                SymbolId = this.SymbolId,
                SymbolType = this.SymbolType,
                SymbolName = this.SymbolName,
                Name = this.Name,
                OpenDate = this.OpenDate,
                OpenPrice = this.OpenPrice,
                OpenWeight = this.OpenWeight,
                TradeType = this.TradeType,
                TradeStatus = this.TradeStatus,
                Dividends = this.Dividends,
                CloseDate = this.CloseDate,
                ClosePrice = this.ClosePrice,
                CurrentPrice = this.CurrentPrice,
                Gain = this.Gain,
                AbsoluteGain = this.AbsoluteGain,
                MaxGain = this.MaxGain,
                LastUpdatePrice = this.LastUpdatePrice,
                LastUpdateDate = this.LastUpdateDate
            };
        }
    }
}
