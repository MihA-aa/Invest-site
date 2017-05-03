using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Enums;

namespace BLL.DTO
{
    public class ViewTemplateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TemplatePositions Positions { get; set; }
        public bool ShowPortfolioStats { get; set; }
        public Sorting SortOrder { get; set; }
    }
}
