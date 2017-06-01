using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BLL.DTO.Enums;

namespace PL.Models
{
    public class ViewTemplateModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Show Positions")]
        public TemplatePositionsDTO Positions { get; set; }

        [Required]
        [Display(Name = "Show Portfolio Stats")]
        public bool ShowPortfolioStats { get; set; }
    }
}