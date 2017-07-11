using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DAL.Entities
{
    public class ColumnFormat
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        private ISet<Format> _formats;

        public virtual ISet<Format> Formats
        {
            get
            {
                return _formats ?? (_formats = new HashSet<Format>());
            }
            set { _formats = value; }
        }
    }

    public class ColumnFormatMap : ClassMapping<ColumnFormat>
    {
        private ColumnFormatMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.Name);
            Set(a => a.Formats,
            c => {
                c.Cascade(Cascade.All);
                c.Key(k => k.Column("ColumnFormatId"));
                c.Table("Format_ColumnFormat"); c.Inverse(true);
            },
            r => r.ManyToMany(m => m.Column("FormatId")));
        }
    }
}
