using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Entities
{
    class Position
    {
        public int Id { get; set; }
        public int SymbolId { get; set; }
        public bool SymbolType { get; set; }
        public virtual string Symbol { get; set; }
        public string Name { get; set; }
        public DateTime OpenDate  { get; set; }
        public decimal OpenPrice { get; set; }
        public int OpenWeight { get; set; }
        public bool TradeType  { get; set; }
        public bool TradeStatus  { get; set; }
        public int Dividends { get; set; }
        public DateTime CloseDate { get; set; }
        public decimal? ClosePrice { get; set; }
        public decimal? CurrentPrice { get; set; }
        public decimal Gain { get; set; }
        public decimal AbsoluteGain { get; set; }
        public decimal MaxGain { get; set; }

        public int PortfolioId { get; set; }
        public virtual Portfolio Portfolio { get; set; }
    }
}
