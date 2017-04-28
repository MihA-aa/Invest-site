using ClassLibrary1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
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
