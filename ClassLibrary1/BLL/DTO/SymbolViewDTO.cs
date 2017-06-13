using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class SymbolViewDTO
    {
        public int SymbolID { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string CurrencySymbol { get; set; }
    }
}
