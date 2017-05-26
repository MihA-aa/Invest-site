using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Views
{
    [Table("SymbolView")]
    public class SymbolView
    {
        [Key]
        public int SymbolID { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
    }
}
