using FluentNHibernate.Mapping;
using NHibernate.AspNet.Identity;
using NHibernate.Mapping;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace DAL.Entities
{
    public class User: IdentityUser
    {
        //public virtual Profile Profile { get; set; }
    }

    //public class UserMap : ClassMap<User>
    //{
    //    private UserMap()
    //    {
    //        Id(x => x.Id);
    //        References(x => x.Profile).Unique().Cascade.All();
    //    }
    //}
}
