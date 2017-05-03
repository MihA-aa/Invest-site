using System.Collections.Generic;

namespace DAL.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public virtual ICollection<Portfolio> Portfolios { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public Customer()
        {
            Portfolios = new List<Portfolio>();
            Users = new List<User>();
        }
    }
}
