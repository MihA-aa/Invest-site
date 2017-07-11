using System.Collections.Generic;
using DAL.Enums;

namespace DAL.Entities
{
    public class ViewTemplate
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual TemplatePositions Positions { get; set; }
        public virtual bool ShowPortfolioStats { get; set; }
        public virtual int? SortColumnId { get; set; }
        public virtual ViewTemplateColumn SortColumn { get; set; }
        public virtual Sorting SortOrder { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<ViewTemplateColumn> Columns { get; set; }
        public virtual ICollection<View> Views { get; set; }
        public ViewTemplate()
        {
            Columns = new List<ViewTemplateColumn>();
            Views = new List<View>();
        }
    }
}
