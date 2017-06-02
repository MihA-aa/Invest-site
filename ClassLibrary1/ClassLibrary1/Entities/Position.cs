using System;
using DAL.Enums;

namespace DAL.Entities
{
    public class Position
    {
        public int Id { get; set; }
        public int SymbolId { get; set; }
        public Symbols SymbolType { get; set; }
        public string SymbolName { get; set; }
        public string Name { get; set; }
        public DateTime OpenDate  { get; set; }
        public decimal OpenPrice { get; set; }
        public int OpenWeight { get; set; }
        public TradeTypes TradeType  { get; set; }
        public TradeStatuses TradeStatus  { get; set; }
        public decimal Dividends { get; set; }
        public DateTime? CloseDate { get; set; }
        public decimal? ClosePrice { get; set; }
        public decimal? CurrentPrice { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public decimal? LastUpdatePrice { get; set; }
        public decimal Gain { get; set; }
        public decimal AbsoluteGain { get; set; }
        public decimal MaxGain { get; set; }
        
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
                MaxGain = this.MaxGain
            };
        }
    }
}
