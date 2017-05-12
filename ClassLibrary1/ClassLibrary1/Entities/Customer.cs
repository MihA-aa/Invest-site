﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Portfolio> Portfolios { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }
        public Customer()
        {
            Portfolios = new List<Portfolio>();
            Profiles = new List<Profile>();
        }
    }
}
