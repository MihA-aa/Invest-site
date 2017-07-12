using System.Collections.Generic;
using DAL.Enums;
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
        public virtual Column Column { get; set; }
        public virtual ColumnFormat ColumnFormat { get; set; }

        private IList<ViewTemplate> _viewTemplates;
        public virtual IList<ViewTemplate> ViewTemplatesForSorting
        {
            get
            {
                return _viewTemplates ?? (_viewTemplates = new List<ViewTemplate>());
            }
            set { _viewTemplates = value; }
        }
    }

    public class ViewTemplateColumnMap : ClassMapping<ViewTemplateColumn>
    {
        private ViewTemplateColumnMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.Name);
            Property(x => x.DisplayIndex);
            Property(x => x.ColumnFormatId);
            Property(x => x.FormatId);
            Property(x => x.ColumnId);
            Property(x => x.ViewTemplateId);
            ManyToOne(x => x.ViewTemplate,
            c => {
                c.Cascade(Cascade.Persist);
                c.Column("ViewTemplate_Id");
            });
            Bag(x => x.ViewTemplatesForSorting,
            c => { c.Key(k => k.Column("SortColumn_Id")); c.Inverse(true); },
            r => r.OneToMany());
            ManyToOne(x => x.Column,
            c => {
                c.Cascade(Cascade.Persist);
                c.Column("Column_Id");
            });
            ManyToOne(x => x.ColumnFormat,
            c => {
                c.Cascade(Cascade.Persist);
                c.Column("ColumnFormat_Id");
            });
        }
    }
}
