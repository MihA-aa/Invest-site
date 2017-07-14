using FluentNHibernate.Mapping;
using NHibernate.AspNet.Identity;
using NHibernate.Identity.Internal.Mapping;
using NHibernate.Mapping;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace DAL.Entities
{
    public class UserEntity: IdentityUser
    {
        //public virtual Profile Profile { get; set; }
    }

    //public class UserMap<T> : IdentityUserMap<User>
    //{
    //    private UserMap()
    //    {

    //        References(x => x.Profile).Unique().Cascade.All();
    //    }
    //}
}
