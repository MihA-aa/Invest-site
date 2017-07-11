using NHibernate.AspNet.Identity;

namespace DAL.Entities
{
    public class User: IdentityUser
    {
        public virtual Profile Profile { get; set; }
    }
}
