using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PL.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}