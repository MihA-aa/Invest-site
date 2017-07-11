using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DAL.Entities
{
    public class Column
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Format Format { get; set; }
    }

    public class ColumnMap : ClassMapping<Column>
    {
        ColumnMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.Name);
            ManyToOne(x => x.Format,
            c => {
                c.Cascade(Cascade.Persist);
                c.Column("Format_Id");
            });
        }
    }
}
