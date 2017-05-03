using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    class ViewTemplateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TemplatePositions Positions { get; set; }
        public bool ShowPortfolioStats { get; set; }
        public Sorting SortOrder { get; set; }
    }
}
