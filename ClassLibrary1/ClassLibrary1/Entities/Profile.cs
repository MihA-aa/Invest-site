using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DAL.Entities
{
    public class Profile
    {
        [Key]
        [ForeignKey("User")]
        public virtual string Id { get; set; }
        public virtual string Login { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual int? CustomerId { get; set; }
        private User _user;
        public virtual User User
        {
            get { return _user ?? (_user = new User()); }
            set { _user = value; }
        }
    }

    public class ProfileMap : ClassMapping<Profile>
    {
        private ProfileMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.Login);
            Property(x => x.CustomerId);
            ManyToOne(x => x.Customer,
            c => {
                c.Cascade(Cascade.Persist);
                c.Column("Customer_Id");
            });
            OneToOne(x => x.User, c => {
                c.Cascade(Cascade.All);
                c.Constrained(true);
            });
        }
    }
}
