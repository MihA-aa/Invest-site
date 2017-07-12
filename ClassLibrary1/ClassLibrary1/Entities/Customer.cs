using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DAL.Entities
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        private IList<Portfolio> _portfolios;
        private IList<Profile> _profiles;
        private IList<View> _views;
        private IList<ViewTemplate> _viewTemplates;

        public virtual IList<Portfolio> Portfolios
        {
            get
            {
                return _portfolios ?? (_portfolios = new List<Portfolio>());
            }
            set { _portfolios = value; }
        }
        public virtual IList<Profile> Profiles
        {
            get
            {
                return _profiles ?? (_profiles = new List<Profile>());
            }
            set { _profiles = value; }
        }
        public virtual IList<View> Views
        {
            get
            {
                return _views ?? (_views = new List<View>());
            }
            set { _views = value; }
        }
        public virtual IList<ViewTemplate> ViewTemplates
        {
            get
            {
                return _viewTemplates ?? (_viewTemplates = new List<ViewTemplate>());
            }
            set { _viewTemplates = value; }
        }
    }

    public class CustomerMap : ClassMapping<Customer>
    {
        CustomerMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.Name);
            Bag(x => x.Portfolios,
            c => { c.Key(k => k.Column("Customer_Id")); c.Inverse(true); },
            r => r.OneToMany());
            Bag(x => x.Profiles,
            c => { c.Key(k => k.Column("Customer_Id")); c.Inverse(true); },
            r => r.OneToMany());
            Bag(x => x.Views,
            c => { c.Key(k => k.Column("Customer_Id")); c.Inverse(true); },
            r => r.OneToMany());
            Bag(x => x.ViewTemplates,
            c => { c.Key(k => k.Column("Customer_Id")); c.Inverse(true); },
            r => r.OneToMany());
        }
    }
}
