using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Customer
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Portfolio> Portfolios { get; set; }
        public virtual User User { get; set; }
        //public virtual ICollection<User> Users { get; set; }
        public Customer()
        {
            Portfolios = new List<Portfolio>();
            //Users = new List<User>();
        }
    }
}
