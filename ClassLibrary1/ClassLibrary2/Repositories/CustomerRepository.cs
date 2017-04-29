using ClassLibrary1.Entities;
using ClassLibrary1.Interfaces;
using ClassLibrary2.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.Repositories
{
    class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
