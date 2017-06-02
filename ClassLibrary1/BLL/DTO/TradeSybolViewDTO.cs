using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class TradeSybolViewDTO
    {
        public int SymbolID { get; set; }
        public DateTime TradeDate { get; set; }
        public decimal TradeIndex { get; set; }
    }
}
