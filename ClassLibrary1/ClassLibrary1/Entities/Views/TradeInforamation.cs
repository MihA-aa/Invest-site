using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Views
{
    public class TradeInforamation
    {
        public DateTime TradeDate { get; set; }
        public decimal Price { get; set; }
        public decimal Dividends { get; set; }
    }
}
