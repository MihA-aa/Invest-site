using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.AspNet.Identity;

namespace DAL.ApplicationManager
{
    public class ApplicationUserManager : UserManager<UserEntity>
    {
        public ApplicationUserManager(IUserStore<UserEntity> store)
                : base(store)
        {
        }
    }
}
