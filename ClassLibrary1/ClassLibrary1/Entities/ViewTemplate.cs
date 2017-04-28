using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Entities
{
    public class ViewTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TemplatePositions Positions { get; set; }
        public bool ShowPortfolioStats { get; set; }
        public bool CollapsedGroups { get; set; }
        public bool SymbolClickCharts { get; set; }
        public bool GroupTrades { get; set; }
        public Sorting SortOrder { get; set; }

        public virtual ICollection<ViewTemplateColumn> Columns { get; set; }
        public virtual ICollection<View> Views { get; set; }
        public ViewTemplate()
        {
            Columns = new List<ViewTemplateColumn>();
            Views = new List<View>();
        }
    }
}
