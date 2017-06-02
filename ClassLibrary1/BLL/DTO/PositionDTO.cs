using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Enums;
using DAL.Enums;

namespace BLL.DTO
{
    public class PositionDTO
    {
        public int Id { get; set; }
        public int SymbolId { get; set; }
        public SymbolsDTO SymbolType { get; set; }
        public string SymbolName { get; set; }
        public string Name { get; set; }
        public DateTime OpenDate { get; set; }
        public decimal OpenPrice { get; set; }
        public int OpenWeight { get; set; }
        public TradeTypesDTO TradeType { get; set; }
        public TradeStatusesDTO TradeStatus { get; set; }
        public decimal Dividends { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public decimal? LastUpdatePrice { get; set; }
        public DateTime? CloseDate { get; set; }
        public decimal? ClosePrice { get; set; }
        public decimal? CurrentPrice { get; set; }
        public decimal Gain { get; set; }
        public decimal AbsoluteGain { get; set; }
        public decimal MaxGain { get; set; }
    }
}
