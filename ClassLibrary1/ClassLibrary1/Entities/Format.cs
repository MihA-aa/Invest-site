using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DAL.Entities
{
    public class Format
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        private ISet<ColumnFormat> _columnFormats;
        private IList<Column> _columns;

        public virtual ISet<ColumnFormat> ColumnFormats
        {
            get
            {
                return _columnFormats ?? (_columnFormats = new HashSet<ColumnFormat>());
            }
            set { _columnFormats = value; }
        }

        public virtual IList<Column> Columns
        {
            get
            {
                return _columns ?? (_columns = new List<Column>());
            }
            set { _columns = value; }
        }
    }

    public class FormatMap : ClassMapping<Format>
    {
        FormatMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.Name);

            Set(a => a.ColumnFormats,
            c => {
                c.Cascade(Cascade.Persist);
                c.Key(k => k.Column("FormatId"));
                c.Table("Format_ColumnFormat");
            },
            r => r.ManyToMany(m => m.Column("ColumnFormatId")));

            Bag(x => x.Columns,
            c => { c.Key(k => k.Column("Format_Id")); c.Inverse(true); },
            r => r.OneToMany());
        }
    }
}
