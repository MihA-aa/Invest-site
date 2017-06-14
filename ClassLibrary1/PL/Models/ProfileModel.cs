using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PL.Models
{
    public class ProfileModel
    {
        public string Id { get; set; }
        [Required]
        public string Login { get; set; }

        [Display(Name = "Customer")]
        [Required]
        public int? CustomerId { get; set; }
    }
}