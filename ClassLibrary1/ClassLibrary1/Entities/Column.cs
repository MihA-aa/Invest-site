using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DAL.Entities
{
    public class Column
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Format Format { get; set; }
        public virtual IList<ViewTemplateColumn> ViewTemplateColumns { get; set; }
    }

    public class ColumnMap : ClassMap<Column>
    {
        public ColumnMap()
        {
            Id(x => x.Id);
            Map(x => x.Name).Not.Nullable().Length(200);
            References(x => x.Format).Column("Format").Not.Nullable();
            HasMany(x => x.ViewTemplateColumns).Inverse().Cascade.All().KeyColumn("ColumnEntiy");
        }
    }
}
