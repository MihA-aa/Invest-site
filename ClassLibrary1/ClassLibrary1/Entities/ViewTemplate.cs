using System.Collections.Generic;
using DAL.Enums;

namespace DAL.Entities
{
    public class ViewTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TemplatePositions Positions { get; set; }
        public bool ShowPortfolioStats { get; set; }
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
