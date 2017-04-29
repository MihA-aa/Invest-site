using ClassLibrary1.Interfaces;
using ClassLibrary2.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ClassLibrary1;

namespace ClassLibrary2.Repositories
{
    class UserRepository : GenericRepository<User>
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
