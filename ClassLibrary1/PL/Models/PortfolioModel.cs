using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PL.Models
{
    public class PortfolioModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Visibility { get; set; }
    }
}