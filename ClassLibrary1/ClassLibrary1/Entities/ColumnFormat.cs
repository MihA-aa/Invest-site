using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DAL.Entities
{
    public class ColumnFormat
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<ViewTemplateColumn> ViewTemplateColumns { get; set; }//many to one
        public virtual IList<Format> Formats { get; set; }//ManyToManyPart>
    }

    public class ColumnFormatMap : ClassMap<ColumnFormat>
    {
        public ColumnFormatMap()
        {

            Id(x => x.Id);
            Map(x => x.Name).Not.Nullable().Length(200);
            HasMany(x => x.ViewTemplateColumns).Inverse().Cascade.All().KeyColumn("ColumnFormat");
            HasManyToMany(x => x.Formats).Cascade.All().Inverse().Table("ColumnFormat_Format");
        }
    }
}
