using DAL.Enums;

namespace DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public virtual Customer Customer { get; set; }
        public Roles Role { get; set; }
    }
}
