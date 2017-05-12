﻿using DAL.Enums;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Entities
{
    public class User: IdentityUser
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        public virtual Profile Profile { get; set; }
        //public Roles Role { get; set; }
    }
}
