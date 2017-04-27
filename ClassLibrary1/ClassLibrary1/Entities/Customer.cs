using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Entities
{
    class Customer
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
