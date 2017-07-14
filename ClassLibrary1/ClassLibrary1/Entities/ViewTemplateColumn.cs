using System.Collections.Generic;
using DAL.Enums;
using FluentNHibernate.Mapping;
using NHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DAL.Entities
{
    public class ViewTemplateColumn
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int DisplayIndex { get; set; }
        public virtual int? ColumnFormatId { get; set; }
        public virtual int? FormatId { get; set; }              //I CAN'T DELETE THIS AND I DON'T KNOW WHY -__-
        public virtual int? ColumnId { get; set; }
        public virtual int? ViewTemplateId { get; set; }
        public virtual ViewTemplate ViewTemplate { get; set; }
        public virtual Column ColumnEntiy { get; set; }
        public virtual ColumnFormat ColumnFormat { get; set; }
        
        public virtual IList<ViewTemplate> ViewTemplatesForSorting { get; set; }

        public ViewTemplateColumn()
        {
            ViewTemplatesForSorting = new List<ViewTemplate>();
        }
    }

    public class ViewTemplateColumnMap : ClassMap<ViewTemplateColumn>
    {
        public ViewTemplateColumnMap()
        {
            Id(x => x.Id);
            Map(x => x.Name).Not.Nullable().Length(200);
            Map(x => x.DisplayIndex).Not.Nullable();
            Map(x => x.ColumnFormatId);
            Map(x => x.FormatId);
            Map(x => x.ColumnId);
            Map(x => x.ViewTemplateId);
            References(x => x.ViewTemplate).Column("ViewTemplate");//.Not.Nullable();
            References(x => x.ColumnEntiy).Column("ColumnEntiy");//.Not.Nullable();
            References(x => x.ColumnFormat).Column("ColumnFormat").Not.Nullable();
            HasMany(x => x.ViewTemplatesForSorting).Inverse().Cascade.All().KeyColumn("SortColumn");
        }
    }
}
