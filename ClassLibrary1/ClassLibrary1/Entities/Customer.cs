using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentNHibernate.Mapping;
using NHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DAL.Entities
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<Portfolio> Portfolios { get; set; }
        public virtual IList<Profile> Profiles { get; set; }
        public virtual IList<ViewForTable> Views { get; set; }
        public virtual IList<ViewTemplate> ViewTemplates { get; set; }
    }

    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(x => x.Id);
            Map(x => x.Name).Not.Nullable().Length(200);
            HasMany(x => x.Portfolios).Inverse().Cascade.All().KeyColumn("Customer");
            HasMany(x => x.Profiles).Inverse().Cascade.All().KeyColumn("Customer");
            HasMany(x => x.Views).Inverse().Cascade.All().KeyColumn("Customer");
            HasMany(x => x.ViewTemplates).Inverse().Cascade.All().KeyColumn("Customer");
        }
    }
}
