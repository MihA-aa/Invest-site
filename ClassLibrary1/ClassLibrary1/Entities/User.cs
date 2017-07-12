using NHibernate.AspNet.Identity;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace DAL.Entities
{
    public class User: IdentityUser
    {
        //public virtual Profile Profile { get; set; }
    }

    //public class UserMap : ClassMapping<User>
    //{
    //    private UserMap()
    //    {
    //        OneToOne(x => x.Profile,
    //        c => c.Cascade(Cascade.None));
    //    }
    //}
}
