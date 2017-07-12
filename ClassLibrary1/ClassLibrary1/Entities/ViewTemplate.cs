using System.Collections.Generic;
using DAL.Enums;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DAL.Entities
{
    public class ViewTemplate
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual TemplatePositions Positions { get; set; }
        public virtual bool ShowPortfolioStats { get; set; }
        public virtual int? SortColumnId { get; set; }
        public virtual Sorting SortOrder { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ViewTemplateColumn SortColumn { get; set; }

        private IList<View> _views;
        private IList<ViewTemplateColumn> _columns;

        public virtual IList<ViewTemplateColumn> Columns
        {
            get
            {
                return _columns ?? (_columns = new List<ViewTemplateColumn>());
            }
            set { _columns = value; }
        }
        public virtual IList<View> Views
        {
            get
            {
                return _views ?? (_views = new List<View>());
            }
            set { _views = value; }
        }
    }

    public class ViewTemplateMap : ClassMapping<ViewTemplate>
    {
        private ViewTemplateMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.Name);
            Property(x => x.Positions);
            Property(x => x.ShowPortfolioStats);
            Property(x => x.SortColumnId);
            Property(x => x.SortOrder);
            ManyToOne(x => x.Customer,
            c => {
                c.Cascade(Cascade.Persist);
                c.Column("Customer_Id");
            });
            Bag(x => x.Views,
            c => { c.Key(k => k.Column("ViewTemplate_Id")); c.Inverse(true); },
            r => r.OneToMany());
            Bag(x => x.Columns,
            c => { c.Key(k => k.Column("ViewTemplate_Id")); c.Inverse(true); },
            r => r.OneToMany());
            ManyToOne(x => x.SortColumn,
            c => {
                c.Cascade(Cascade.Persist);
                c.Column("SortColumn_Id");
            });
        }
    }
}
