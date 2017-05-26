using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Views
{
    [Table("SymbolDividends")]
    public class SymbolDividend
    {
        [Key]
        public int SymbolID { get; set; }
        public decimal Dividends { get; set; }
    }
}
