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

        private IList<ViewTemplateColumn> _viewTemplateColumns;
        public virtual IList<ViewTemplateColumn> ViewTemplateColumns
        {
            get
            {
                return _viewTemplateColumns ?? (_viewTemplateColumns = new List<ViewTemplateColumn>());
            }
            set { _viewTemplateColumns = value; }
        }
    }

    public class ColumnMap : ClassMapping<Column>
    {
        public ColumnMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.Name);
            ManyToOne(x => x.Format,
            c => {
                c.Cascade(Cascade.Persist);
                c.Column("Format_Id");
            });
            Bag(x => x.ViewTemplateColumns,
            c => { c.Key(k => k.Column("Column_Id")); c.Inverse(true); },
            r => r.OneToMany());
        }
    }
}
