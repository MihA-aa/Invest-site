using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Enums;

namespace BLL.DTO
{
    class ViewDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ShowName { get; set; }
        public DateFormats DateFormat { get; set; }
        public int MoneyPrecision { get; set; }
        public int PercentyPrecision { get; set; }
    }
}
