using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Dividend
    {
        public int Id { get; set; }
        public string ShareholderName { get; set; }
        public decimal Price { get; set; }
        public virtual Symbol Symbol { get; set; }
    }
}
