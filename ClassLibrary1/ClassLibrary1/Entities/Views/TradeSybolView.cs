using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Views
{
    [Table("TradeSybolInformation")]
    public class TradeSybolView
    {
        [Key]
        public int SymbolID { get; set; }
        public DateTime TradeDate { get; set; }
        public decimal TradeIndex { get; set; }
    }
}
