using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.DTO.Enums;

namespace PL.Models
{
    public class ViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Display(Name = "Show Name")]
        public bool ShowName { get; set; }

        [Required]
        [Display(Name = "Date Format")]
        public DateFormatsDTO DateFormat { get; set; }

        [Required]
        [Display(Name = "View Template")]
        public int? ViewTemplateId { get; set; }

        public int? PortfolioId { get; set; }

        [Display(Name = "Money Precision")]
        public int MoneyPrecision { get; set; }

        [Display(Name = "Percent Precision")]
        public int PercentyPrecision { get; set; }
    }
}