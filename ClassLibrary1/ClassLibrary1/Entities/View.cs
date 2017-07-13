using DAL.Enums;
using FluentNHibernate.Mapping;
using NHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DAL.Entities
{
    public class ViewForTable
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool ShowName { get; set; }
        public virtual DateFormats DateFormat { get; set; }
        public virtual int MoneyPrecision { get; set; }
        public virtual int PercentyPrecision { get; set; }
        public virtual int? ViewTemplateId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ViewTemplate ViewTemplate { get; set; }
    }

    public class ViewMap : ClassMap<ViewForTable>
    {
        public ViewMap()
        {
            Id(x => x.Id);
            Map(x => x.Name).Length(200);
            Map(x => x.ShowName).Not.Nullable();
            Map(x => x.DateFormat).Not.Nullable();
            Map(x => x.MoneyPrecision).Not.Nullable();
            Map(x => x.PercentyPrecision).Not.Nullable();
            Map(x => x.ViewTemplateId);
            References(x => x.Customer).Column("Customer").Not.Nullable();
            References(x => x.ViewTemplate).Column("ViewTemplate").Not.Nullable();
        }
    }
}
