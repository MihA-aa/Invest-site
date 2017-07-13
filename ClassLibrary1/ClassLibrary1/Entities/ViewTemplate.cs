using System.Collections.Generic;
using DAL.Enums;
using FluentNHibernate.Mapping;
using NHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DAL.Entities
{
    public class ViewTemplate
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual TemplatePositions Positions { get; set; }
        public virtual bool ShowPortfolioStats { get; set; }
        public virtual int? SortColumnId { get; set; }
        public virtual Sorting SortOrder { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ViewTemplateColumn SortColumn { get; set; }

        public virtual IList<ViewTemplateColumn> Columns { get; set; }
        public virtual IList<ViewForTable> Views { get; set; }
    }

    public class ViewTemplateMap : ClassMap<ViewTemplate>
    {
        public ViewTemplateMap()
        {
            Id(x => x.Id);
            Map(x => x.Name).Not.Nullable().Length(200);
            Map(x => x.Positions).Not.Nullable();
            Map(x => x.ShowPortfolioStats).Not.Nullable();
            Map(x => x.SortColumnId);
            Map(x => x.SortOrder).Not.Nullable();
            References(x => x.Customer).Column("Customer").Not.Nullable();
            References(x => x.SortColumn).Column("SortColumn");
            HasMany(x => x.Views).Inverse().Cascade.All().KeyColumn("Views");
            HasMany(x => x.Columns).Inverse().Cascade.All().KeyColumn("Columns");
        }
    }
}
