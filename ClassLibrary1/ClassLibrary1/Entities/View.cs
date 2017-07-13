using DAL.Enums;
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

    public class ViewMap : ClassMapping<ViewForTable>
    {
        public ViewMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.Name);
            Property(x => x.ShowName);
            Property(x => x.DateFormat);
            Property(x => x.MoneyPrecision);
            Property(x => x.PercentyPrecision);
            Property(x => x.ViewTemplateId);
            ManyToOne(x => x.Customer,
            c =>
            {
                c.Cascade(Cascade.Persist);
                c.Column("Customer_Id");
            });
            ManyToOne(x => x.ViewTemplate,
            c =>
            {
                c.Cascade(Cascade.Persist);
                c.Column("ViewTemplate_Id");
            });
        }
    }
}
