using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplicationForTestingAPI.Models.Enum;

namespace ConsoleApplicationForTestingAPI.Models
{
    public class PositionModel
    {
        public int Id { get; set; }
        public int SymbolId { get; set; }
        public Symbols SymbolType { get; set; }
        public string SymbolName { get; set; }
        public string Name { get; set; }
        public DateTime OpenDate { get; set; }
        public decimal? OpenPrice { get; set; }
        public int OpenWeight { get; set; }
        public TradeTypes TradeType { get; set; }
        public TradeStatuses TradeStatus { get; set; }
        public DateTime? CloseDate { get; set; }
        public decimal? ClosePrice { get; set; }
        public decimal? CurrentPrice { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public decimal? LastUpdatePrice { get; set; }
        public decimal? Gain { get; set; }
        public decimal? AbsoluteGain { get; set; }
        public decimal? MaxGain { get; set; }
        public decimal Dividends { get; set; }
        public string CurrencySymbol { get; set; }
    }
}
