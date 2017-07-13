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
    public class Format
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<ColumnFormat> ColumnFormats { get; set; } //ManyToMany
        public virtual IList<Column> Columns { get; set; }
    }

    public class FormatMap : ClassMap<Format>
    {
        public FormatMap()
        {
            Id(x => x.Id);
            Map(x => x.Name).Not.Nullable().Length(200);
            HasManyToMany(x => x.ColumnFormats).Cascade.All().Table("Format_ColumnFormat");
            HasMany(x => x.Columns).Inverse().Cascade.All().KeyColumn("Format");
        }
    }
}
