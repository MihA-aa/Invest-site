using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHibernate.Mapping;
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
        //public virtual User User { get; set; }
    }

    public class ProfileMap : ClassMap<Profile>
    {
        public ProfileMap()
        {
            Id(x => x.Id);//.GeneratedBy.Guid();
            Map(x => x.Login).Length(200);
            Map(x => x.CustomerId);
            References(x => x.Customer).Column("Customer").Not.Nullable();
            //HasOne(x => x.User).Cascade.All().PropertyRef("User");
        }
    }
}
