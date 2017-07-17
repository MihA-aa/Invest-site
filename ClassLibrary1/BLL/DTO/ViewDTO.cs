using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Enums;
using DAL.Enums;

namespace BLL.DTO
{
    public class ViewDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ShowName { get; set; }
        public DateFormatsDTO DateFormat { get; set; }
        public int? ViewTemplateId { get; set; }
        public int MoneyPrecision { get; set; }
        public int PercentyPrecision { get; set; }
    }
}
